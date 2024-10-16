﻿using System;
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
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
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
            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                return View(await _CourseDomain.GetCourses(User.FindFirst("CollegeCode").Value));
            }
            else
            {
                
                return View(await _CourseDomain.GetAllCourses());
            }
                
            

        }

       



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Collages = await _CourseDomain.GetCollages();
            ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");



            var Levels = await _CourseDomain.GetLevels();
            ViewBag.LevelsList = new SelectList(Levels, "Id", "LevelNameAr");

            if(User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            }
            else
            {
                var Majors = await _CourseDomain.GetAllMajors();
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            }
           

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

            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            }
            else
            {
                var Majors = await _CourseDomain.GetAllMajors();
                ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
            }


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
                var Collages = await _CourseDomain.GetCollages();
                ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");

                var Levels = await _CourseDomain.GetLevels();
                ViewBag.LevelsList = new SelectList(Levels, "Id", "LevelNameAr");

                if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                {
                    var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
                    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                }
                else
                {
                    var Majors = await _CourseDomain.GetAllMajors();
                    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                }
                if (ModelState.IsValid)
                {
                    if (await _CourseDomain.CourseNumEx(model.GUID, model.CourseNum))
                    {

                        ModelState.AddModelError("CourseNum", "هذا المقرر موجود مسبقاً");
                        return View(model);

                    }
                    if (model.CourseNum == "0")
                    {

                        ModelState.AddModelError("CourseNum", "خطأ في رمز المقرر");
                        return View(model);

                    }
                    
                    if(User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                    {
                        int check = await _CourseDomain.addCourse(model, User.FindFirst(ClaimTypes.NameIdentifier).Value, User.FindFirst("CollegeCode").Value);
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
                        int check = await _CourseDomain.addAllCourse(model, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                        if (check == 1)
                        {
                            ViewData["Successful"] = "تمت الإضافة بنجاح";
                        }
                        else
                        {
                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                        }
                    }

                    
                    //return RedirectToAction(nameof(Index));
                }

            }
            catch
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseViewModel model)
        {
            try
            {
                var Collages = await _CourseDomain.GetCollages();
                ViewBag.CollagesList = new SelectList(Collages, "Id", "CollegeNameAr");

                var Levels = await _CourseDomain.GetLevels();
                ViewBag.LevelsList = new SelectList(Levels, "Id", "LevelNameAr");

                if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                {
                    var Majors = await _CourseDomain.GetMajors(User.FindFirst("CollegeCode").Value);
                    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                }
                else
                {
                    var Majors = await _CourseDomain.GetAllMajors();
                    ViewBag.MajorsList = new SelectList(Majors, "Id", "MajorNameAr");
                }

                if (ModelState.IsValid)
                {
                    if (await _CourseDomain.CourseNumEx(model.GUID, model.CourseNum))
                    {
                        ModelState.AddModelError("CourseNum", "هذا المقرر موجود مسبقاً");
                        return View(model);
                    }

                    if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
                    {
                        int check = await _CourseDomain.UpdateCourse(model, User.FindFirst(ClaimTypes.NameIdentifier).Value, User.FindFirst("CollegeCode").Value);

                        if (check == 1)
                        {
                            ViewData["Successful"] = "تم التعديل بنجاح";
                        }
                        else
                        {
                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                        }
                    }
                    else
                    {
                        int check = await _CourseDomain.UpdateAllCourse(model, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                        if (check == 1)
                        {
                            ViewData["Successful"] = "تم التعديل بنجاح";
                        }
                        else
                        {
                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                        }
                    }
                    

                }
            }
            catch
            {

                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Guid id)
        {

            try
            {
                int check = await _CourseDomain.DeleteCourse(id, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (check == 1)
                {
                    ViewData["Successful"] = "تم الحذف بنجاح";
                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                }
                // return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }

            if(User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                var courses = await _CourseDomain.GetCourses(User.FindFirst("CollegeCode").Value);
                return View(courses);
            }
            else
            {
                var courses = await _CourseDomain.GetAllCourses();
                return View(courses);
            }
            
            


        }

        [HttpGet]
        public async Task<IActionResult> GetMajorsByCollege(int collegeId)
        {
            var majors = await _CourseDomain.GetMajorsByCollegeId(collegeId);

            var majorList = majors.Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.MajorNameAr
            }).ToList();

            return Json(majorList);  // Return the list of majors in JSON format
        }


    }
}
