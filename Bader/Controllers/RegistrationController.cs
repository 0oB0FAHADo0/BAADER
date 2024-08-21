using Bader.Domain;
using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Bader.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly RegistrationDomain _RegistrationDomain;

        public RegistrationController(RegistrationDomain registrationDomain)
        {
            _RegistrationDomain = registrationDomain;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var claims = new List<Claim>
                     {
                     new Claim(ClaimTypes.Name, "user4"),
                     new Claim(ClaimTypes.Role, "nothing"),
                     new Claim(ClaimTypes.NameIdentifier, "nothing"),
                     new Claim("PhoneNumber", "123456789"), 
                     new Claim("NameAr", "حسن"),
                     new Claim("NameEn", "Hassan")
                     };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);



                var domainInfo = await _RegistrationDomain.GetAllRegistrations();
                return View(domainInfo);
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(Guid id , string CheckWhichFunction )
        {
            if (CheckWhichFunction == "cancel")
            {
                try
                {
                    RegistrationViewModel Reg = await _RegistrationDomain.GetRegByGuid(id);

                    Reg.RegistrationStateId = 2;
                    int check = await _RegistrationDomain.UpdateRegistration(Reg);

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
            }else if(CheckWhichFunction == "ReActive")
            {
                try
                {
                    RegistrationViewModel Reg = await _RegistrationDomain.GetRegByGuid(id);

                    int checkNumOfStudent = await _RegistrationDomain.CheckCountReg(Reg.SessionId);
                    if (checkNumOfStudent == 1)
                    {
                        Reg.RegistrationStateId = 1;
                        int check = await _RegistrationDomain.UpdateRegistration(Reg);

                        if (check == 1)
                        {
                            ViewData["Successful"] = " .تم إعادة التسجيل بنجاح";

                        }
                        else
                        {

                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                        }
                    }
                    else
                    {

                        ViewData["Falied"] = "الجلسة ممتلئة.";

                    }
                }
                catch
                {
                    ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                }
            }
     
            

            var domainInfo = await _RegistrationDomain.GetAllRegistrations();
            return View(domainInfo);
        }



        [HttpGet]
        public async Task<IActionResult> Create(Guid id)
        {
            try
            {
                ViewBag.username = User.FindFirst(ClaimTypes.Name).Value;
                ViewBag.fullnameAr = User.FindFirst("NameAr").Value;
                ViewBag.NameEn = User.FindFirst("NameEn").Value;
                ViewBag.PhoneNumber = User.FindFirst("PhoneNumber").Value;

                var  session = await _RegistrationDomain.GetSessionsById(id);
                ViewBag.SessionNameAr = session.SessionNameAr;
                ViewBag.CourseNameAr = session.CourseNameAr;
                ViewBag.TitleAr = session.TitleAr;
                ViewBag.NumOfStudents = session.NumOfStudents;
                ViewBag.SessionDate = session.SessionDate;
                ViewBag.RegStartDate = session.RegStartDate;
                ViewBag.RegEndDate = session.RegEndDate;
                ViewBag.sessionID =  _RegistrationDomain.GetSessionsIdByGUId(session.GUID); 




            }
            catch
            {
                return View();
            }

            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Create(RegistrationViewModel Reg)
        {
            

            
            try
            {
                ViewBag.username = User.FindFirst(ClaimTypes.Name).Value;
                ViewBag.fullnameAr = User.FindFirst("NameAr").Value;
                ViewBag.NameEn = User.FindFirst("NameEn").Value;
                ViewBag.PhoneNumber = User.FindFirst("PhoneNumber").Value;
                var session = await _RegistrationDomain.GetSessionByIdNotGuid(Reg.SessionId);
                ViewBag.SessionNameAr = session.SessionNameAr;
                ViewBag.CourseNameAr = session.CourseNameAr;
                ViewBag.TitleAr = session.TitleAr;
                ViewBag.NumOfStudents = session.NumOfStudents;
                ViewBag.SessionDate = session.SessionDate;
                ViewBag.RegStartDate = session.RegStartDate;
                ViewBag.RegEndDate = session.RegEndDate;
                ViewBag.sessionID = _RegistrationDomain.GetSessionsIdByGUId(session.GUID);

                if (ModelState.IsValid)
                {
                    Reg.Username= User.FindFirst(ClaimTypes.Name).Value;
                    Reg.FullNameAr = User.FindFirst("NameAr").Value;
                    Reg.FullNameEn=User.FindFirst("NameEn").Value;
                    Reg.Phone = User.FindFirst("PhoneNumber").Value;
                    if (await _RegistrationDomain.DidUserRegBefore(Reg.Username, Reg.SessionId))
                    {

                        ModelState.AddModelError("Username", "انت مسجل بالفعل.");
                        ViewData["NoRegForYou"] = "انت مسجل بالفعل.";



                        return View(Reg);
                    }
                    int checkNumOfStudent = await _RegistrationDomain.CheckCountReg(Reg.SessionId);
                    if (checkNumOfStudent == 0)
                    {
                        
                        ViewData["NoRegForYou"] = "الجلسة ممتلئة.";

                        return View(Reg);
                    }

                    if (await _RegistrationDomain.CheckForreActiveInCreate(Reg.Username, Reg.SessionId))
                    {
                        var guid = _RegistrationDomain.GetGuidByUsername(Reg.Username);
                        Reg.GUID = guid;
                        Reg.RegistrationStateId = 1;
                        Reg.RegDate = DateTime.Now;
                        int check = await _RegistrationDomain.UpdateRegistration(Reg);

                        if (check == 1)
                        {
                            ViewData["Successful"] = "تم التسجيل في الجلسة بنجاح.";

                        }
                        else
                        {

                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                        }
                    }
                    else
                    {
                        int check = await _RegistrationDomain.AddRegistration(Reg);
                        if (check == 1)
                        {
                            ViewData["Successful"] = "تم التسجيل بالجلسة بنجاح.";

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
                return View();

            }


            return View();
        }





        public async Task<IActionResult> SessionsRegCards()
        {
            try
            {



                var domainInfo = await _RegistrationDomain.GetSessions();
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



                ViewBag.username = reg.Username;
                ViewBag.fullnameAr = reg.FullNameAr;
                ViewBag.NameEn = reg.FullNameEn;
                ViewBag.PhoneNumber = reg.Phone;
                var session = await _RegistrationDomain.GetSessionByIdNotGuid(reg.SessionId);
                ViewBag.SessionNameAr = session.SessionNameAr;
                ViewBag.CourseNameAr = session.CourseNameAr;
                ViewBag.TitleAr = session.TitleAr;
                ViewBag.NumOfStudents = session.NumOfStudents;
                ViewBag.SessionDate = session.SessionDate;
                ViewBag.RegStartDate = session.RegStartDate;
                ViewBag.RegEndDate = session.RegEndDate;



                return View();
            }
            catch
            {
                return View();
            }
        }

    }
}
