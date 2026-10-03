using Microsoft.AspNetCore.Mvc;

namespace Project_Hospital_System_Task11.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}