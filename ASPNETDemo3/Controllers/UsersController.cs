using ASPNETDemo3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETDemo3.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                         View(await _context.Users.ToListAsync()) :
                         Problem("Entity set 'ApplicationDbContext.Lobbies'  is null.");
        }
    }
}
