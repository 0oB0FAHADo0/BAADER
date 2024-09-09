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
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;


namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin, SuperAdmin")]
    [Authorize]
    public class LevelsController : Controller
    {
        private readonly LevelDomain _LevelDomain;

        public LevelsController(LevelDomain context)
        {
            _LevelDomain = context;
        }
        //[Authorize(Policy = "CollegeCodePolicy19")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var DomainInfo = await _LevelDomain.GetLevels();



            return View(DomainInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Guid id)
        {

            try
            {
                LevelViewModel Level = await _LevelDomain.GetLevelbyId(id);

                Level.IsDeleted = true;
                int check = await _LevelDomain.UpdateLevel(Level);

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



            var DomainInfo = await _LevelDomain.GetLevels();
            return View(DomainInfo);
        }




        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Create(LevelViewModel Level)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    if (await _LevelDomain.LevelNumExists(Level.LevelNum, Level.GUID))
                    {
                        ModelState.AddModelError("LevelNum", "رقم المستوى مستخدم من قبل");
                        return View(Level);
                    }
                    int check = await _LevelDomain.AddLevel(Level);
                    if (check == 1)
                    {
                        ViewData["Successful"] = "تم الإضافة بنجاح";

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


            return View(Level);



        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            try
            {
                LevelViewModel Level = await _LevelDomain.GetLevelbyId(id);
                return View(Level);
            }
            catch
            {

            }




            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LevelViewModel Level)
        {
            try
            {

                int check = await _LevelDomain.UpdateLevel(Level);
                if (check == 1)
                {
                    ViewData["Successful"] = "تم التعديل بنجاح";

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





            return View(Level);

        }
    }
}

