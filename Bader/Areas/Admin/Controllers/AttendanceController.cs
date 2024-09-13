using Bader.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class AttendanceController : Controller
    {

        private readonly AttendanceDomain _AttendanceDomain;

        public AttendanceController(AttendanceDomain AttendanceDomain)
        {
            _AttendanceDomain = AttendanceDomain;
        }

        public async Task<IActionResult> Index()
        {

            var DomainInfo = await _AttendanceDomain.GetAllAttendance();
            return View(DomainInfo);
        }
    }
}
