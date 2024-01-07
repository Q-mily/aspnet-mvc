using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETDemo3.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using ASPNETDemo3.Models;
using System.Drawing.Printing;

namespace ASPNETDemo3.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var lobbies = _context.Lobbies.Where(l => l.status > 0).ToList();
            ViewBag.lobbies = lobbies;
            var filter = new OrderFilterModel
            {
                TotalCount = 0,
                PageSize = 6,
                Page = int.TryParse(Request.Query["page"], out int page_param) ? page_param : 1,
                search = Request.Query["search"],
                status = int.TryParse(Request.Query["status"], out int status_param) ? status_param : null,
                lobbyId = int.TryParse(Request.Query["lobbyId"], out int lobbyId_param) ? lobbyId_param : null,
                DateFrom = DateTime.TryParse(Request.Query["DateFrom"], out DateTime DateFrom_param) ? DateFrom_param : DateTime.MinValue,
                DateTo = DateTime.TryParse(Request.Query["DateTo"], out DateTime DateTo_param) ? DateTo_param : DateTime.MinValue
            };
            ViewBag.FilterModel = filter;

            IQueryable<Order> query = _context.Orders.Include(o => o.Lobby)
                .Where(order =>
                    (string.IsNullOrEmpty(filter.search) || EF.Functions.Like(order.customerName, $"%{filter.search}%")) &&
                    (filter.status == null || order.status == filter.status) &&
                    (filter.lobbyId == null || order.lobbyId == filter.lobbyId) &&
                    (filter.DateFrom == DateTime.MinValue || order.dateAt >= filter.DateFrom) &&
                    (filter.DateTo == DateTime.MinValue || order.dateAt <= filter.DateTo) &&
                    order.status > -1);

            int totalCount = query.Count();
            filter.TotalCount = totalCount;

            var applicationDbContext = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return View(await applicationDbContext);
        }

        // GET: Orders/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Lobby)
                .Include(o => o.OrderTables)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            List<Lobby> lobbies = _context.Lobbies.ToList();
            ViewBag.Lobbies = lobbies;
            ViewBag.order = order;
            return View(order);
            //var json = JsonSerializer.Serialize(order, new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            //    //IgnoreReadOnlyProperties = true,
            //    //WriteIndented = true
            //});
            //return Json(json);
        }

        // GET: Orders/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewData["lobbyId"] = new SelectList(_context.Lobbies, "Id", "Id");
            var lobbies = _context.Lobbies.ToList();
            ViewBag.lobbies = lobbies;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,lobbyId,customerName,customerPhone,groomName,brideName,ca,dateAt,status,createdAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return Json(ModelState);
            }
            ViewData["lobbyId"] = new SelectList(_context.Lobbies, "Id", "Id", order.lobbyId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["lobbyId"] = new SelectList(_context.Lobbies, "Id", "Id", order.lobbyId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,lobbyId,customerName,customerPhone,groomName,brideName,ca,dateAt,status,createdAt")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.Alert = "Cập nhật thành công";
                return RedirectToAction("Details", new { id = order.Id });
            }
            ViewData["lobbyId"] = new SelectList(_context.Lobbies, "Id", "Id", order.lobbyId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Lobby)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.status = -1;
                _context.Update(order);
                //_context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.status = 2;
                _context.Update(order);
                //_context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = order.Id });
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.status = 0;
                _context.Update(order);
                //_context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new {id = order.Id });
        }

        public async Task<IActionResult> Payment(int? id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.Include(o => o.OrderTables).FirstOrDefaultAsync(o => o.Id == id);
            if (order != null)
            {
                order.status = 3;
                order.TotalPrice = order.OrderTables.Sum(t => t.TotalPrice);
                order.PaymentAt = DateTime.Now;
                _context.Update(order);
                //_context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
