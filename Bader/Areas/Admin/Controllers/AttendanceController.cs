using Bader.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
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
