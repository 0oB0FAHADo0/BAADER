using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bader.Models;
using Bader.Domain;
using Bader.ViewModels;
using System.Linq.Expressions;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Bader.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseDomain _CourseDomain;

        public CourseController(CourseDomain context)
        {
            _CourseDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _CourseDomain.GetCourses());
        }

        [HttpGet]
        public async Task<IActionResult> UserIndex()
        {
            return View(await _CourseDomain.GetSomeCourses());
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Collages = await _CourseDomain.GetCollages();
            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");
            
            var Levels = await _CourseDomain.GetLevels();
            ViewBag.LevelsList = new SelectList(Levels, "Id", "LevelNameAr");

            return View(new CourseViewModel());
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _CourseDomain.GetCourseById(id);

            var Collages = await _CourseDomain.GetCollages();
            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");

            var Levels = await _CourseDomain.GetLevels();
            ViewBag.LevelsList = new SelectList(Levels, "Id", "LevelNameAr");


            if (user == null)
            {
                return NotFound();
            }

        

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _CourseDomain.CourseNumEx(model.GUID, model.CourseNum))
                    {

                        return Json(new { success = false , Message = "هذا المقرر موجود مسبقاً." });

                    }

                    int check = await _CourseDomain.addCourse(model);
                    if(check == 1)
                    {
                        return Json(new { success = true , message = "تمت الإضافة بنجاح." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
                    }
                    //return RedirectToAction(nameof(Index));
                }
                
            }
            catch
            {
                return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
            }
            
            

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _CourseDomain.CourseNumEx(model.GUID, model.CourseNum))
                    {
                        return Json(new { success = false , message = "هذا المقرر موجود مسبقاً" });
                      //  return View(model);
                    }

                    int check = await _CourseDomain.UpdateCourse(model);

                    if(check == 1)
                    {
                        return Json(new { success = true, message = "تم التحديث بنجاح" });
                    }
                    else
                    {
                        return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
                    }
                   
                }
            }
            catch {

                return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Guid id)
        {

            try
            {
               int check = await _CourseDomain.DeleteCourse(id);

                if(check == 1)
                {
                    return Json(new { success = true, message = "تم الحذف بنجاح" });
                }
                else
                {
                    return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
                }
               // return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                return Json(new { success = false, message = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق" });
            }
            
        }



    }
}
