using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETDemo3.Data;

namespace ASPNETDemo3.Controllers
{
    public class LobbiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LobbiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lobbies
        public async Task<IActionResult> Index()
        {
              return _context.Lobbies != null ? 
                          View(await _context.Lobbies.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lobbies'  is null.");
        }

        // GET: Lobbies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lobbies == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobby == null)
            {
                return NotFound();
            }

            return View(lobby);
        }

        // GET: Lobbies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lobbies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MinCountTable,ImagePath,Description")] Lobby lobby)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lobby);
        }

        // GET: Lobbies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lobbies == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies.FindAsync(id);
            if (lobby == null)
            {
                return NotFound();
            }
            return View(lobby);
        }

        // POST: Lobbies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MinCountTable,ImagePath,Description")] Lobby lobby)
        {
            if (id != lobby.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lobby);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LobbyExists(lobby.Id))
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
            return View(lobby);
        }

        // GET: Lobbies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lobbies == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lobby == null)
            {
                return NotFound();
            }

            return View(lobby);
        }

        // POST: Lobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lobbies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lobbies'  is null.");
            }
            var lobby = await _context.Lobbies.FindAsync(id);
            if (lobby != null)
            {
                _context.Lobbies.Remove(lobby);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LobbyExists(int id)
        {
          return (_context.Lobbies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
