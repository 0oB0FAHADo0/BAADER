using Bader.Domain;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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

            ViewBag.lastTwoYear = DateTime.Now.Year - 2;
            ViewBag.Now = DateTime.Now.Year;
            ViewBag.AfterTwoYear = DateTime.Now.Year + 2;
            ViewBag.AfterFourYear = DateTime.Now.Year + 4;
            ViewBag.AfterSixYear = DateTime.Now.Year + 6;
            ViewBag.LastTwo = 0;
            ViewBag.AfterTwo = 0;
            ViewBag.AfterFour = 0;
            ViewBag.AfterSix = 0;
            ViewBag.Noww = 0;

            foreach (var sessiondate in SessionDate)
            {
                if (sessiondate.SessionDate.Year == ViewBag.lastTwoYear || sessiondate.SessionDate.Year == ViewBag.lastTwoYear + 1)
                {
                    ViewBag.lastTwo += 1;
                }
                else if (sessiondate.SessionDate.Year == ViewBag.Now || sessiondate.SessionDate.Year == ViewBag.Now + 1)
                {
                    ViewBag.Noww += 1;
                }
                else if (sessiondate.SessionDate.Year == ViewBag.AfterTwoYear || sessiondate.SessionDate.Year == ViewBag.AfterTwoYear + 1)
                {
                    ViewBag.AfterTwo += 1;
                }
                else if (sessiondate.SessionDate.Year == ViewBag.AfterFourYear || sessiondate.SessionDate.Year == ViewBag.AfterFourYear + 1)
                {
                    ViewBag.AfterFour += 1;
                }
                else
                {
                    ViewBag.AfterSix += 1;
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
