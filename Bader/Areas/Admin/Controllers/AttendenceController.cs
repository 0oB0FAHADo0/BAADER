using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    public class AttendenceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
