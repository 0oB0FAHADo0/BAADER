using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Bader.Controllers
{
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
            return View(await _ContentDomain.GetContents());
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
            try
            {
                if (ModelState.IsValid)
                {
                    int check =await _ContentDomain.AddContent(content , User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
                var Courses = await _ContentDomain.GetCourses();
                ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
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
            var Courses = await _ContentDomain.GetCourses();
            ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
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
                    int check=await _ContentDomain.UpdateContent(content , User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
                 var Courses = await _ContentDomain.GetCourses();
                ViewBag.CoursesList = new SelectList(Courses, "Id", "CourseNameAr");
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

    }
}
