﻿using Microsoft.AspNetCore.Mvc;
using Bader.Domain;
using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class CollegeController : Controller
    {
        private readonly CollegeDomain _CollegeDomain;

        public CollegeController(CollegeDomain collegeDomain)
        {
            _CollegeDomain = collegeDomain;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var DomainInfo = await _CollegeDomain.GetAllColleges();

              

            return View(DomainInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Guid id)
        {

            try
            {
                CollegeViewModel College = await _CollegeDomain.GetCollegebyId(id);

                College.IsDeleted = true;
                int check = await _CollegeDomain.UpdateCollege(College);

                if (check == 1)
                {
                    ViewData["Successful"] = "تم الحذف بنجاح";

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



            var DomainInfo = await _CollegeDomain.GetAllColleges();
            return View(DomainInfo);
        }




        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        public async Task<IActionResult> Add(CollegeViewModel college)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    if (await _CollegeDomain.CollegeCodeExists(college.CollegeCode, college.GUID))
                    {
                        ModelState.AddModelError("CollegeCode", "لا يمكن ان يوجد رقم كلية متكرر");
                        return View(college);
                    }

                    int check = await _CollegeDomain.AddCollege(college);
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


            return View(college);



        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            try
            {
                CollegeViewModel College = await _CollegeDomain.GetCollegebyId(id);
                return View(College);
            }
            catch
            {

            }




            return View();
        }




        public async Task<IActionResult> Edit(CollegeViewModel college)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (await _CollegeDomain.CollegeCodeExists(college.CollegeCode, college.GUID))
                    {
                        ModelState.AddModelError("CollegeCode", "لا يمكن ان يوجد رقم كلية متكرر");
                        return View(college);
                    }

                    int check = await _CollegeDomain.UpdateCollege(college);
                    //return RedirectToAction("Details");

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
            catch
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }





            return View(college);

        }




    }
}


