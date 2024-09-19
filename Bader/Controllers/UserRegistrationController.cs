using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bader.Controllers
{
   
    public class UserRegistrationController : Controller
    {

        private readonly RegistrationDomain _RegistrationDomain;
        private readonly UserDomain _UserDomain;
        private readonly AttendanceDomain _AttendanceDomain;

        public UserRegistrationController(RegistrationDomain registrationDomain, UserDomain userDomain, AttendanceDomain attendanceDomain)
        {
            _RegistrationDomain = registrationDomain;
            _UserDomain = userDomain;
            _AttendanceDomain = attendanceDomain;
        }

        public async Task<IActionResult> UserIndex()
        {
            try
            {
                ViewBag.datecurrsnt = DateTime.Now;
                var domainInfo = await _RegistrationDomain.GetAllRegistrationsByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                
                return View(domainInfo);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserIndex(Guid id, string CheckWhichFunction)
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



            var domainInfo = await _RegistrationDomain.GetAllRegistrationsByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(domainInfo);
        }


        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            try
            {
                RegistrationViewModel reg = new RegistrationViewModel();
                var userInfo = await _UserDomain.GetUsersByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                reg.Email = userInfo.Email;
                reg.Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                reg.FullNameAr = User.FindFirst("FullNameAr").Value;
                reg.FullNameEn = User.FindFirst("FullNameEn").Value;
                reg.Phone = User.FindFirst("Phone").Value;

                var session = await _RegistrationDomain.GetSessionsById(id);


                reg.SessionNameAr = session.SessionNameAr;
                reg.CourseNameAr = session.CourseNameAr;
                reg.TitleAr = session.TitleAr;
                reg.NumOfStudents = session.NumOfStudents;
                reg.SessionDate = session.SessionDate /*?? default(DateTime)*/;
                reg.RegStartDate = session.RegStartDate /*?? default(DateTime)*/;
                reg.RegEndDate = session.RegEndDate/*?? default(DateTime)*/;
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


                    int check = await _RegistrationDomain.AddRegistration(reg, User.FindFirst(ClaimTypes.NameIdentifier).Value);


                    if (check == 1)
                    {
                        int regId = await _RegistrationDomain.GetIdByUsernameForReg(reg.Username, reg.SessionId);

                        AttendanceViewModel Attend = new AttendanceViewModel();
						Attend.SessionId = reg.SessionId;
						Attend.UserName = reg.Username;
						Attend.SessionDate = reg.SessionDate;
                        Attend.RegistrationId = regId;

                        
						int check2 = await _AttendanceDomain.addStudentInAteend(Attend);


                        if (check2 == 1)
                        {
                            ViewData["Successful"] = "تم التسجيل بالجلسة بنجاح.";
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
            }
            catch
            {
                return View();

            }


            return View();
        }





        public async Task<IActionResult> SessionsRegCards()
        {
            try
            {
                var userInfo = await _UserDomain.GetUsersByUsername(User.FindFirst(ClaimTypes.NameIdentifier).Value);



                var domainInfo = await _RegistrationDomain.GetSessions(User.FindFirst("CollegeCode").Value,userInfo.Gender);
                return View(domainInfo);
            }
            catch
            {
                return View();
            }
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
                reg.NumOfStudents = session.NumOfStudents ;
                reg.SessionDate = session.SessionDate/* ?? default(DateTime)*/;
                reg.RegStartDate = session.RegStartDate /*?? default(DateTime)*/;
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
