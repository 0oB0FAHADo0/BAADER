﻿
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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]
    public class SessionController : Controller
    {
        private readonly SessionDomain _SessionsDomain;

        public SessionController(SessionDomain context)
        {
            _SessionsDomain = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _SessionsDomain.GetSessions());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid Id)
        {
            SessionsViewModel session = await _SessionsDomain.GetSessionsById(Id);
            session.IsDeleted = true;

            int check = await _SessionsDomain.UpdateSessions(session, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (check == 1)
            {
                ViewData["Successful"] = "تم الحذف بنجاح";

            }
            else
            {

                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }


            return View(await _SessionsDomain.GetSessions());


        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var Session = await _SessionsDomain.getFromSessionsState();
            ViewBag.SessionList = new SelectList(Session, "Id", "StateAr");

            var Course = await _SessionsDomain.getFromtblCourses();
            ViewBag.CourseList = new SelectList(Course, "Id", "CourseNameAr");
            return View();
        }

        // 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionsViewModel session)
        {



            try
            {
                //  if (session.RegEndDate<session.RegStartDate) {
                var SessionState = await _SessionsDomain.getFromSessionsState();
                ViewBag.SessionList = new SelectList(SessionState, "Id", "StateAr");

                var Course = await _SessionsDomain.getFromtblCourses();

                if (ModelState.IsValid)
                {
                    int check = await _SessionsDomain.AddSessions(session, User.FindFirst(ClaimTypes.NameIdentifier).Value);
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

            return View();

        }

        //ed
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var session = await _SessionsDomain.GetSessionsById(id);
            if (session == null)
            {
                return NotFound();
            }
            var Session = await _SessionsDomain.getFromSessionsState();
            ViewBag.SessionList = new SelectList(Session, "Id", "StateAr");

            var Course = await _SessionsDomain.getFromtblCourses();
            ViewBag.CourseList = new SelectList(Course, "Id", "CourseNameAr");
            return View(session);


        }

        // POST:Edit
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(SessionsViewModel session)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    int check = await _SessionsDomain.UpdateSessions(session, User.FindFirst(ClaimTypes.NameIdentifier).Value);
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
            return View();
            // return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index");

        }

    }



}



