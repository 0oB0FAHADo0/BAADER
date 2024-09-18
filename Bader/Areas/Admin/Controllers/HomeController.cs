using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CourseDomain _CourseDomain;
        private readonly CollegeDomain _collegeDomain;

        public HomeController(CourseDomain context, CollegeDomain collegeDomain)
        {
            _CourseDomain = context;
            _collegeDomain = collegeDomain;
        }
        public async Task <IActionResult> Index()
        {
            var Coursecount = await _CourseDomain.GetAllCourses();
            ViewBag.CourseCount = Coursecount.Count();
            var colleges = await _collegeDomain.GetAllColleges();

            ViewBag.CourseList = new List<int>();
            ViewBag.CollegeName = new List<string>();

            foreach (var college in colleges)
            {
                var coursebycollege = await _CourseDomain.GetCoursesCount(college.CollegeCode);
                ViewBag.CourseList.Add(coursebycollege);
                ViewBag.CollegeName.Add(college.CollegeNameAr);
            }
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
