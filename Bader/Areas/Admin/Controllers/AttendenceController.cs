using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AttendenceController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
