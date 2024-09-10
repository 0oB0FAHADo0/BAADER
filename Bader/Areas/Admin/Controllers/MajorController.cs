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
