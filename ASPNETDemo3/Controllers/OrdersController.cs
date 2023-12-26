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
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.Orders.Include(o => o.Lobby);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
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
        public IActionResult Create()
        {
            ViewData["lobbyId"] = new SelectList(_context.Lobbies, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                order.status = 0;
                _context.Add(order);
                //_context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> AddOrderTables(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Lobby)
                .Include(o => o.OrderTables)
                .FirstOrDefaultAsync(m => m.Id == id);
            //var orderTables = await _context.OrderTables.Where(o => o.OrderId == order.Id).ToListAsync();
            if (order == null)
            {
                return NotFound();
            }

            List<Lobby> lobbies = _context.Lobbies.ToList();
            ViewBag.Lobbies = lobbies;
            return View(order);
        }
    }
}
