using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETDemo3.Data;
using ASPNETDemo3.Models;
using NuGet.Packaging;

namespace ASPNETDemo3.Controllers
{
    public class OrderTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OrderTables.Include(o => o.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderTables == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrderTables
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderTable == null)
            {
                return NotFound();
            }

            return View(orderTable);
        }

        // GET: OrderTables/Create
        public async Task<IActionResult> Create()
        {
            ViewData["OrderId"] = Request.Query["OrderId"];
            List<Food> foods = _context.Foods.ToList();
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == int.Parse(Request.Query["OrderId"]));
            
            ViewBag.Foods = foods;
            ViewBag.order = order;
            var model = new DataOrderTable
            {
                OrderTable = new OrderTable()
            };
            return View(model);
        }

        // POST: OrderTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DataOrderTable dataOrderTable)
        {

            if (ModelState.IsValid)
            {
                var OrderTable = dataOrderTable.OrderTable;
                var FoodIds = dataOrderTable.FoodIds.ToList();
                var Foods = _context.Foods.Where(f => FoodIds.Contains(f.Id)).ToList();

                var UnitPrice = 0;
                var TotalPrice = 0;
                UnitPrice = Foods.Sum(food => food.Price);
                TotalPrice = (int)(UnitPrice * OrderTable.Amount);

                OrderTable.UnitPrice = UnitPrice;
                OrderTable.TotalPrice = TotalPrice;

                if (OrderTable != null && Foods.Any())
                {
                    _context.Add(OrderTable);

                    foreach (var food in Foods)
                    {
                        // Ensure that the Food entities are tracked or explicitly set their state
                        // _context.Entry(food).State = EntityState.Added;
                        OrderTable.Foods.Add(food);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Orders", new { id = OrderTable.OrderId});
                }

                //_context.Add(OrderTable);
                //await _context.SaveChangesAsync();

                //if (Foods.Any())
                //{
                //    OrderTable.Foods.AddRange(Foods);
                //    await _context.SaveChangesAsync();
                //}    
                
                //return RedirectToAction(nameof(Index));

            }
            else
            {
                return Json(ModelState);
            }
            ViewData["OrderId"] = Request.Query["OrderId"];
            List<Food> foods = _context.Foods.ToList();
            ViewBag.Foods = foods;
            return View(dataOrderTable);
        }

        // GET: OrderTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderTables == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrderTables.FindAsync(id);
            if (orderTable == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderTable.OrderId);
            return View(orderTable);
        }

        // POST: OrderTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderId,table_name,UnitPrice,Amount,TotalPrice,status,createdAt")] OrderTable orderTable)
        {
            if (id != orderTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderTableExists(orderTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderTable.OrderId);
            return View(orderTable);
        }

        // GET: OrderTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderTables == null)
            {
                return NotFound();
            }

            var orderTable = await _context.OrderTables
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderTable == null)
            {
                return NotFound();
            }

            return View(orderTable);
        }

        // POST: OrderTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderTables == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderTables'  is null.");
            }
            var orderTable = await _context.OrderTables.FindAsync(id);
            if (orderTable != null)
            {
                _context.OrderTables.Remove(orderTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderTableExists(int id)
        {
          return (_context.OrderTables?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
