using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bader.Controllers
{
    public class ContentsController : Controller
    {
        private readonly ContentDomain _ContentDomain;

        public ContentsController(ContentDomain context)
        {
            _ContentDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _ContentDomain.GetContents());
        }
        public async Task<IActionResult> Create()
        {
            var Courses = await _ContentDomain.GetCourses();
            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentViewModel content)
        {
            if (ModelState.IsValid)
            {
                await _ContentDomain.AddContent(content);
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _ContentDomain.GetCourses();
            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            return View(content);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var content = await _ContentDomain.GetContentByGUID(guid);
            if (content == null)
            {
                return NotFound();
            }
            var Courses = await _ContentDomain.GetCourses();
            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            return View(content);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ContentViewModel content)
        {
            if (ModelState.IsValid)
            {
                await _ContentDomain.UpdateContent(content);
                return RedirectToAction(nameof(Index));
            }
            var Courses = await _ContentDomain.GetCourses();
            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            return View(content);
        }

    }
}
