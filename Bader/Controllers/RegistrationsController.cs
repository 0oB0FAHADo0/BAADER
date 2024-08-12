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
    public class RegistrationsController : Controller
    {

        private readonly RegistrationDomain _RegistrationDomain;

        public RegistrationsController(RegistrationDomain registrationDomain)
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
                        ViewData["Successful"] = "تم الغاء التسجيل بنجاح";

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
                            ViewData["Successful"] = "تم إعادة التسجيل بنجاح";

                        }
                        else
                        {

                            ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

                        }
                    }
                    else
                    {

                        ViewData["Falied"] = "الجلسه ممتلئة بالفعل";

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
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.username = User.FindFirst(ClaimTypes.Name).Value;
                ViewBag.fullnameAr = User.FindFirst("NameAr").Value;
                ViewBag.NameEn = User.FindFirst("NameEn").Value;
                ViewBag.PhoneNumber = User.FindFirst("PhoneNumber").Value;


                var SessionsInfo = await _RegistrationDomain.GetSessions();
                ViewBag.SessuonsList = new SelectList(SessionsInfo, "Id", "SessionNameAr");

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
            ViewBag.username = User.FindFirst(ClaimTypes.Name).Value;
            ViewBag.fullnameAr = User.FindFirst("NameAr").Value;
            ViewBag.NameEn = User.FindFirst("NameEn").Value;
            ViewBag.PhoneNumber = User.FindFirst("PhoneNumber").Value;

            try
            {
                if (ModelState.IsValid)
                {
                    if (await _RegistrationDomain.DidUserRegBefore(Reg.Username, Reg.SessionId))
                    {

                        ModelState.AddModelError("Username", "لا يمكن للمستخدم التسجيل بالجلسه اكثر من مره");
                        var SessionsInfo = await _RegistrationDomain.GetSessions();
                        ViewBag.SessuonsList = new SelectList(SessionsInfo, "Id", "SessionNameAr");
                        return View(Reg);
                    }
                    int checkNumOfStudent = await _RegistrationDomain.CheckCountReg(Reg.SessionId);
                    if (checkNumOfStudent == 0)
                    {
                        ModelState.AddModelError("SessionId", "الجلسه ممتلئه");
                        var SessionsInfo = await _RegistrationDomain.GetSessions();
                        ViewBag.SessuonsList = new SelectList(SessionsInfo, "Id", "SessionNameAr");
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
                            ViewData["Successful"] = "تم الغاء التسجيل بنجاح";

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
                            ViewData["Successful"] = "تمت الإضافة بنجاح";

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
    }
}
