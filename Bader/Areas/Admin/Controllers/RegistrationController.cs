using Bader.Domain;
using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
//using Bader.Migrations;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RegistrationController : Controller
    {

        private readonly RegistrationDomain _RegistrationDomain;
        private readonly UserDomain _UserDomain;
        private readonly AttendanceDomain _AttendanceDomain;

        public RegistrationController(RegistrationDomain registrationDomain, UserDomain userDomain, AttendanceDomain AttendanceDomain)
        {
            _RegistrationDomain = registrationDomain;
            _UserDomain = userDomain;
            _AttendanceDomain = AttendanceDomain;

		}

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.datecurrsnt = DateTime.Now;

                _RegistrationDomain.UpdateSessionStates();
                _RegistrationDomain.UpdateRegStates();
                var UserRole= User.FindFirst(ClaimTypes.Role).Value;
                if (UserRole== "Admin" || UserRole == "Editor")
                {
                    var domainInfo = await _RegistrationDomain.GetAllRegistrationsByCollegeCode(User.FindFirst("CollegeCode").Value);

                    return View(domainInfo);
                }
                else
                {


                    var domainInfo = await _RegistrationDomain.GetAllRegistrations();

                    return View(domainInfo);

                }


            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid id, string CheckWhichFunction)
        {
            if (CheckWhichFunction == "cancel")
            {
                try
                {
                    RegistrationViewModel Reg = await _RegistrationDomain.GetRegByGuid(id);

                    Reg.RegistrationStateId = 2;
                    int check = await _RegistrationDomain.UpdateRegistration(Reg, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    if (check == 1)
                    {
                        ViewData["Successful"] = "تم إلغاء التسجيل بنجاح.";

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
            }
            //else if(CheckWhichFunction == "ReActive")
            //{
            //    try
            //    {
            //        RegistrationViewModel Reg = await _RegistrationDomain.GetRegByGuid(id);

            //        int checkNumOfStudent = await _RegistrationDomain.CheckCountReg(Reg.SessionId);
            //        if (checkNumOfStudent == 1)
            //        {
            //            Reg.RegistrationStateId = 1;
            //            int check = await _RegistrationDomain.UpdateRegistration(Reg);

            //            if (check == 1)
            //            {
            //                ViewData["Successful"] = " .تم إعادة التسجيل بنجاح";

            //            }
            //            else
            //            {

            //                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            //            }
            //        }
            //        else
            //        {

            //            ViewData["Falied"] = "الجلسة ممتلئة.";

            //        }
            //    }
            //    catch
            //    {
            //        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            //    }
            //}


            var UserRole = User.FindFirst(ClaimTypes.Role).Value;
            if (UserRole == "Admin" || UserRole == "Editor")
            {
                var domainInfo = await _RegistrationDomain.GetAllRegistrationsByCollegeCode(User.FindFirst("CollegeCode").Value);

                return View(domainInfo);
            }
            else
            {
                var domainInfo = await _RegistrationDomain.GetAllRegistrations();

                return View(domainInfo);

            }
        }





        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            try
            {
                RegistrationViewModel reg = new RegistrationViewModel();

                var userInfo = await _UserDomain.GetUsersByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                reg.Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                reg.FullNameAr = User.FindFirst("FullNameAr").Value;
                reg.FullNameEn = User.FindFirst("FullNameEn").Value;
                reg.Phone = User.FindFirst("Phone").Value;
                reg.Email = userInfo.Email; 


                var session = await _RegistrationDomain.GetSessionsById(id);


                reg.SessionNameAr = session.SessionNameAr;
                reg.CourseNameAr = session.CourseNameAr;
                reg.TitleAr = session.TitleAr;
                reg.NumOfStudents = session.NumOfStudents??0;
                reg.SessionDate = session.SessionDate ;
                reg.RegStartDate = session.RegStartDate;
                reg.RegEndDate = session.RegEndDate;
                reg.SessionId = _RegistrationDomain.GetSessionsIdByGUId(session.GUID);



                return View(reg);


            }
            catch
            {
                return View();
            }




        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationViewModel reg)
        {



            try
            {
                var userInfo = await _UserDomain.GetUsersByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                reg.Email = userInfo.Email;

                reg.Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                reg.FullNameAr = User.FindFirst("FullNameAr").Value;
                reg.FullNameEn = User.FindFirst("FullNameEn").Value;
                reg.Phone = User.FindFirst("Phone").Value;
                var session = await _RegistrationDomain.GetSessionByIdNotGuid(reg.SessionId);

                reg.SessionId = _RegistrationDomain.GetSessionsIdByGUId(session.GUID);

                if (ModelState.IsValid)
                {

                    if (await _RegistrationDomain.DidUserRegBefore(reg.Username, reg.SessionId))
                    {

                        ModelState.AddModelError("Username", "انت مسجل بالفعل.");
                        ViewData["NoRegForYou"] = "انت مسجل بالفعل.";



                        return View(reg);
                    }
                    int checkNumOfStudent = await _RegistrationDomain.CheckCountReg(reg.SessionId);
                    if (checkNumOfStudent == 0)
                    {

                        ViewData["NoRegForYou"] = "الجلسة ممتلئة.";

                        return View(reg);
                    }

                    //if (await _RegistrationDomain.CheckForreActiveInCreate(Reg.Username, Reg.SessionId))
                    //{
                    //    var guid = _RegistrationDomain.GetGuidByUsername(Reg.Username);
                    //    Reg.GUID = guid;
                    //    Reg.RegistrationStateId = 1;
                    //    Reg.RegDate = DateTime.Now;
                    //    int check = await _RegistrationDomain.UpdateRegistration(Reg);

                    //    if (check == 1)
                    //    {
                    //        ViewData["Successful"] = "تم التسجيل في الجلسة بنجاح.";

                    //    }
                    //    else
                    //    {

                    //        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                    //    }
                    //}

                    int check = await _RegistrationDomain.AddRegistration(reg, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (check == 1)
                    {

                        AttendanceViewModel Attend = new AttendanceViewModel();

                        Attend.SessionId = reg.SessionId;
                        Attend.UserName = reg.Username;
                        Attend.SessionDate = reg.SessionDate;


                        int check2 = await _AttendanceDomain.addStudentInAteend(Attend);
                        ViewData["Successful"] = "تم التسجيل بالجلسة بنجاح.";

                    }
                    else
                    {
                        ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                    }




                }
            }
            catch
            {
                return View();

            }


            return View();
        }





        public async Task<IActionResult> RegDetails(Guid id)
        {
            try
            {
                var reg = await _RegistrationDomain.GetRegByGuid(id);




                var session = await _RegistrationDomain.GetSessionByIdNotGuid(reg.SessionId);
                reg.SessionNameAr = session.SessionNameAr;
                reg.CourseNameAr = session.CourseNameAr;
                reg.TitleAr = session.TitleAr;
                reg.NumOfStudents = session.NumOfStudents??0;
                reg.SessionDate = session.SessionDate;
                reg.RegStartDate = session.RegStartDate;
                reg.RegEndDate = session.RegEndDate;




                return View(reg);
            }
            catch
            {
                return View();
            }
        }






    }
}
