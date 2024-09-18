using Bader.Domain;
using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor, SuperAdmin")]
    public class ContentController : Controller
    {
        private readonly ContentDomain _ContentDomain;

        public ContentController(ContentDomain context)
        {
            _ContentDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                return View(await _ContentDomain.GetContentsByCollegeCode(User.FindFirst("CollegeCode").Value));
            }
            else
            {
                return View(await _ContentDomain.GetContents());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid id)
        {
            try
            {
                ContentViewModel Content = await _ContentDomain.GetContentByGUID(id);
                Content.IsDeleted = true;
                int check = await _ContentDomain.UpdateContent(Content, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                if (check == 1)
                {
                    ViewData["Successful"] = "تم الحذف بنجاح";
                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                return View(await _ContentDomain.GetContentsByCollegeCode(User.FindFirst("CollegeCode").Value));
            }
            else
            {
                return View(await _ContentDomain.GetContents());
            }
        }
        public async Task<IActionResult> Create()
        {
            if (User.FindFirst("Role").Value == "SuperAdmin")
            {
                var Collages = await _ContentDomain.GetColleges();
                ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");
                var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                return View();
            }
            else
            {
                var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                //var Courses = await _ContentDomain.GetCoursesByCollegeCode(User.FindFirst("CollegeCode").Value);
                //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentViewModel content)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _ContentDomain.AddContent(content, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    //return RedirectToAction(nameof(Index));
                    if (check == 1)
                    {
                        ViewData["Successful"] = "تمت الإضافة بنجاح";
                    }
                    else
                    {
                        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                        if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                        {
                            //var Courses = await _ContentDomain.GetCoursesByCollegeCode(User.FindFirst("CollegeCode").Value);
                            //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                            var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                            ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                            var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                        }
                        else
                        {
                            //var Courses = await _ContentDomain.GetCourses();
                            //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                            var Collages = await _ContentDomain.GetColleges();
                            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");
                            var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                            ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                            var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                        }
                    }
                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                    if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                    {
                        //var Courses = await _ContentDomain.GetCoursesByCollegeCode(User.FindFirst("CollegeCode").Value);
                        //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                        var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                        ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                        var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                        ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                    }
                    else
                    {
                        //var Courses = await _ContentDomain.GetCourses();
                        //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                        var Collages = await _ContentDomain.GetColleges();
                        ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");
                        var Majors = await _ContentDomain.GetMajorsByCollegeCode(User.FindFirst("CollegeCode").Value);
                        ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                        var Courses = await _ContentDomain.GetCoursesByMajorId(15);
                        ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
            return View(content);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var content = await _ContentDomain.GetContentByGUID(id);
            if (content == null)
            {
                return NotFound();
            }
            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                //var Courses = await _ContentDomain.GetCoursesByCollegeCode(User.FindFirst("CollegeCode").Value);
                //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            }
            else
            {
                //var Courses = await _ContentDomain.GetCourses();
                //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
            }
            return View(content);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ContentViewModel content)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int check = await _ContentDomain.UpdateContent(content, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    //return RedirectToAction(nameof(Index));
                    if (check == 1)
                    {
                        ViewData["Successful"] = "تمت الإضافة بنجاح";
                    }
                    else
                    {
                        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                    }
                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                    if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                    {
                        //var Courses = await _ContentDomain.GetCoursesByCollegeCode(User.FindFirst("CollegeCode").Value);
                        //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                    }
                    else
                    {
                        //var Courses = await _ContentDomain.GetCourses();
                        //ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
            return View(content);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _ContentDomain.GetContentByGUID(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ContentViewModel content)
        {

            await _ContentDomain.DeleteContent(content, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> GetMajorsByCollegeId(int collegeId)
        {
         
                var majors= await _ContentDomain.GetMajorsByCollegeId(collegeId);
                var majorList = majors.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.MajorNameAr
                }).ToList();

                return Json(majorList);  // Return the list of majors in JSON format


        }
        public async Task<IActionResult> GetCoursesByMajorId(int majorId)
        {

            var courses = await _ContentDomain.GetCoursesByMajorId(majorId);
            var courseList = courses.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.CourseNameAr
            }).ToList();

            return Json(courseList);  // Return the list of majors in JSON format


        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var content = await _ContentDomain.GetContentByGUID(id);
            if (content == null)
            {
                return NotFound();
            }
            return View(content);
        }

    }
}
