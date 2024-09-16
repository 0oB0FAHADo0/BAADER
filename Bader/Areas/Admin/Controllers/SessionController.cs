
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
    [Authorize(Roles = "Admin, Editor, SuperAdmin")]
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
            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                return View(await _SessionsDomain.GetSomeSessions(User.FindFirst("CollegeCode").Value));
            }
            else
            {
                return View(await _SessionsDomain.GetSessions());
            }
            //return View(await _SessionsDomain.GetSessions());
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

            if (User.FindFirst("Role").Value == "Admin" || User.FindFirst("Role").Value == "Editor")
            {
                var sessionx = await _SessionsDomain.GetSomeSessions(User.FindFirst("CollegeCode").Value);
                return View(sessionx);
            }
            else
            {
                var sessionx = await _SessionsDomain.GetSessions();
                return View(sessionx);
            }

            //return View(await _SessionsDomain.GetSessions());


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

            if (session.RegStartDate >= session.SessionDate)
            {
                ModelState.AddModelError("RegStartDate", ".يجب أن يكون تاريخ بداية التسجيل قبل تاريخ الجلسة");
            }

            if (session.RegEndDate >= session.SessionDate)
            {
                ModelState.AddModelError("RegEndDate", ".يجب أن يكون تاريخ انتهاء التسجيل قبل  تاريخ الجلسة");
            }

            if (session.RegEndDate <= session.RegStartDate)
            {
                ModelState.AddModelError("RegEndDate", ".يجب أن يكون تاريخ انتهاء التسجيل بعد تاريخ بداية التسجيل");
            }
            //if (session.NumOfStudents = "0")

            //{
            //    ModelState.AddModelError("NumOfStudents", ".يجب أن يكون تاريخ انتهاء التسجيل بعد تاريخ بداية التسجيل");
            //}

            if (!ModelState.IsValid)
            {

                var SessionState = await _SessionsDomain.getFromSessionsState();
                ViewBag.SessionList = new SelectList(SessionState, "Id", "StateAr");

                var Course = await _SessionsDomain.getFromtblCourses();
                ViewBag.CourseList = new SelectList(Course, "Id", "CourseNameAr");
                return View(session);
            }

            //else
            //{
                int check = await _SessionsDomain.AddSessions(session, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (check == 1)
                {
                    ViewData["Successful"] = "تمت الإضافة بنجاح";
                }
                else
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
                }

              return View(session);
          

            //}
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

            if (session.RegStartDate >= session.SessionDate)
            {
                ModelState.AddModelError("RegStartDate", ".يجب أن يكون تاريخ بداية التسجيل قبل تاريخ الجلسة");
            }

            if (session.RegEndDate >= session.SessionDate)
            {
                ModelState.AddModelError("RegEndDate", ".يجب أن يكون تاريخ انتهاء التسجيل قبل  تاريخ الجلسة");
            }

            if (session.RegEndDate <= session.RegStartDate)
            {
                ModelState.AddModelError("RegEndDate", ".يجب أن يكون تاريخ انتهاء التسجيل بعد تاريخ بداية التسجيل");
            }
            //if (session.SessionDate >= DateTime.Today)
            //{
            //    ModelState.AddModelError("SessionDate", ".يجب أن يكون تاريخ انتهاء التسجيل بعد تاريخ بداية التسجيل");
            //}
            //if (session.RegStartDate >= DateTime.Today)
            //{
            //    ModelState.AddModelError("RegStartDate", ".يجب أن يكون تاريخ بداية التسجيل قبل تاريخ الجلسة");
            //}
            //if (session.RegEndDate >= DateTime.Today)
            //{
            //    ModelState.AddModelError("RegEndDate", ".يجب أن يكون تاريخ انتهاء التسجيل قبل  تاريخ الجلسة");
            //}

            if (!ModelState.IsValid)
            {

                var SessionState = await _SessionsDomain.getFromSessionsState();
                ViewBag.SessionList = new SelectList(SessionState, "Id", "StateAr");

                var Course = await _SessionsDomain.getFromtblCourses();
                ViewBag.CourseList = new SelectList(Course, "Id", "CourseNameAr");
                return View(session);
            }

            else {
            int check = await _SessionsDomain.UpdateSessions(session, User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (check == 1)
            {
                ViewData["Successful"] = "تم التعديل بنجاح";
            }
            else
            {
                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";
            }
                return View(session);


                 }
        }

        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var ses = await _SessionsDomain.GetSessionsById(id);


                //var Session = await _SessionsDomain.getFromSessionsState();
                ////ViewBag.SessionList = new SelectList(Session, "Id", "StateAr");

                //var Course = await _SessionsDomain.getFromtblCourses();
                ////ViewBag.CourseList = new SelectList(Course, "Id", "CourseNameAr");

                //ViewBag.SessionNameAr = ses.SessionNameAr;
                //ViewBag.SessionNameEn = ses.SessionNameEn;
                //ViewBag.StateAr =
                //ViewBag.CourseNameAr =
                //ViewBag.TitleAr = ses.TitleAr;
                //ViewBag.TitleEn = ses.TitleEn;
                //ViewBag.Links = ses.Links;
                //ViewBag.NumOfStudents = ses.NumOfStudents;
                //ViewBag.SessionDate = ses.SessionDate;
                //ViewBag.RegStartDate = ses.RegStartDate;
                //ViewBag.RegEndDate = ses.RegEndDate;



                return View(ses);
            }
            catch
            {
                return View();
            }
        }

    }



}



