using Microsoft.AspNetCore.Mvc;

namespace ASPNETDemo3.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
