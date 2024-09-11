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
using System.IO.Compression;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class MajorController : Controller
    {
        private readonly MajorDomain _MajorDomain;

        public MajorController(MajorDomain context)
        {
            _MajorDomain = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _MajorDomain.GetMajors());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Guid id)
        {

            try
            {
                int check = await _MajorDomain.DeleteMajors(id/*, User.FindFirst(ClaimTypes.NameIdentifier).Value*/);

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
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }

            var Major = await _MajorDomain.GetMajors();
            return View(Major);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Collages = await _MajorDomain.GetCollages();
            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");


            return View(new MajorViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MajorViewModel major)
        {
            try
            {
                var Collages = await _MajorDomain.GetCollages();
                ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");

                if (ModelState.IsValid)
                {

                    

                    int check = await _MajorDomain.addMajors(major);
                    if (check == 1)
                    {
                        ViewData["Successful"] = "تمت الإضافة بنجاح";

                    }
                    else
                    {
                        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                    }


                    // return RedirectToAction("Details");

                }
            }
            catch
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }



            return View(major);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var major = await _MajorDomain.GetMajorsById(id);

            var Collages = await _MajorDomain.GetCollages();
            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");

            

            //if (User.FindFirst("Role").Value == "Admin")
            //{
            //    var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
            //    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            //}
            //else
            //{
            //    var Majors = await _CourseDomain.GetAllMajors();
            //    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            //}


            if (major == null)
            {
                return NotFound();
            }



            return View(major);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MajorViewModel major)
        {
            try
            {
                var Collages = await _MajorDomain.GetCollages();
                ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");



                //if (User.FindFirst("Role").Value == "Admin")
                //{
                //    var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
                //    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                //}
                //else
                //{
                //    var Majors = await _CourseDomain.GetAllMajors();
                //    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                //}

                //if (ModelState.IsValid)
                //{
                //    if (await _MajorDomain.CourseNumEx(model.GUID, model.CourseNum))
                //    {
                //        ModelState.AddModelError("CourseNum", "هذا المقرر موجود مسبقاً");
                //        return View(model);
                //    }

                //if (User.FindFirst("Role").Value == "Admin")
                
                if (ModelState.IsValid)
                 { 
               
                        int check = await _MajorDomain.UpdateMajors(major/*,*//* User.FindFirst(ClaimTypes.NameIdentifier).Value, User.FindFirst("CollegeCode").Value*/);
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

                 
                }


                
            }
            catch
            {

                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
            return View(major);
        }
    }
}
