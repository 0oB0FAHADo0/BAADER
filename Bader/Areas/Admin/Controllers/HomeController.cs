using Bader.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CourseDomain _CourseDomain;

        public HomeController(CourseDomain context)
        {
            _CourseDomain = context;
        }
        public async Task <IActionResult> Index()
        {
            var Coursecount = await _CourseDomain.GetAllCourses();
            ViewBag.CourseCount = Coursecount.Count();
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
