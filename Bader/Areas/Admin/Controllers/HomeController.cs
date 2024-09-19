using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly CourseDomain _CourseDomain;
        private readonly CollegeDomain _collegeDomain;
        private readonly UserDomain _userDomain;
        private readonly PermissionDomain _PermissionDomain;
        private readonly SessionDomain _sessionDomain;

        public HomeController(CourseDomain context, CollegeDomain collegeDomain, UserDomain userDomain, PermissionDomain permissionDomain , SessionDomain sessionDomain)
        {
            _CourseDomain = context;
            _collegeDomain = collegeDomain;
            _userDomain = userDomain;
            _PermissionDomain = permissionDomain;
            _sessionDomain = sessionDomain;
        }
        public async Task <IActionResult> Index()
        {
            var PerCount = await _PermissionDomain.GetPermissions();
         
            ViewBag.AdminCount = 0;

            foreach (var role in PerCount)
            {
                if (role.RoleId == 1) {
                    ViewBag.AdminCount = ViewBag.AdminCount + 1;
                }
            }

            var UsersCount = await _userDomain.GetUsers();
            ViewBag.UsersCount = UsersCount.Count();

            var CourseCount = await _CourseDomain.GetAllCourses();
            ViewBag.CourseCount = CourseCount.Count();

            var SessionCount = await _sessionDomain.GetSessions();
            ViewBag.SessionCount = SessionCount.Count();

            ViewBag.Open = 0;
            ViewBag.Close = 0;
            foreach (var sessionstate in SessionCount) { 
            
                if ( sessionstate.StateAr == "متاح")
                {
                    ViewBag.Open = ViewBag.Open + 1;
                }
                else
                {
                    ViewBag.Close = ViewBag.Close + 1;
                }
            }
            var SessionDate = await _sessionDomain.GetSessionsdate();
            ViewBag.SessionDate = SessionDate.Count();

            ViewBag.Session2022 = 0;
            ViewBag.Session2024 = 0;
            ViewBag.Session2026 = 0;
            ViewBag.Session2028 = 0;
            ViewBag.Session2030 = 0;
            foreach ( var sessiondate in SessionDate)
            {
                if ( sessiondate.SessionDate.Year == 2022 || sessiondate.SessionDate.Year == 2023)
                {
                    ViewBag.Session2022 += 1;
                }
                else if(sessiondate.SessionDate.Year == 2024 || sessiondate.SessionDate.Year == 2025)
                {
                    ViewBag.Session2024 += 1;
                }
                else if (sessiondate.SessionDate.Year == 2026 || sessiondate.SessionDate.Year == 2027)
                {
                    ViewBag.Session2026 += 1;
                }
                else if (sessiondate.SessionDate.Year == 2028 || sessiondate.SessionDate.Year == 2029)
                {
                    ViewBag.Session2028 += 1;
                }
                else
                {
                    ViewBag.Session2030 += 1;
                }
            }

            var colleges = await _collegeDomain.GetAllColleges();

            ViewBag.CourseList = new List<int>();
            ViewBag.CollegeName = new List<string>();

            foreach (var college in colleges)
            {
                var coursebycollege = await _CourseDomain.GetSomeCourses(college.CollegeCode);
                ViewBag.CourseList.Add(coursebycollege.Count());
                ViewBag.CollegeName.Add(college.CollegeNameAr);
            }

            return View();
        }
        public IActionResult Profile()
        {
            ViewBag.UserName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View();
        }
    }
}
