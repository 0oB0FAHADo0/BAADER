using Bader.Domain;
using Bader.Migrations;
using Bader.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Security.Claims;
using System.IO;

namespace Bader.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class AttendanceController : Controller
    {

        private readonly AttendanceDomain _AttendanceDomain;
        private readonly SessionDomain _SessionDomain;

        public AttendanceController(AttendanceDomain AttendanceDomain, SessionDomain sessionDomain)
        {
            _AttendanceDomain = AttendanceDomain;
            _SessionDomain = sessionDomain;
        }

        public async Task<IActionResult> Index()
        {





            var UserRole = User.FindFirst(ClaimTypes.Role).Value;
            if (UserRole == "Admin" || UserRole == "Editor")
            {

                var DomainInfo = await _AttendanceDomain.GetSessionsByCollegeCode(User.FindFirst("CollegeCode").Value);

                return View(DomainInfo);
            }
            else
            {

                var DomainInfo = await _AttendanceDomain.GetSessions();

                return View(DomainInfo);

            }
        }


        public async Task<IActionResult> Attend(Guid? id)
        {

            var domaininfo = await _AttendanceDomain.GetAllAttendanceBySeesionGuid(id);


            return View(domaininfo);
        }


        [HttpPost]
        public async Task<IActionResult> Attend(IEnumerable<AttendanceViewModel> attendx)
        {
            int check = 0;
            foreach (var attend in attendx) {
                int iscount = await _AttendanceDomain.updateAttend(attend.GUID, attend.IsAttend,attend.UserName, User.FindFirst(ClaimTypes.NameIdentifier).Value);
                check = check + iscount;

            }
            if (check == attendx.Count())
            {
                ViewData["Successful"] = "تم التحضير بنجاح.";

            } else if (check < attendx.Count() && check != 0)
            {
                ViewData["Falied"] = "حدث خطأ في تحضير بعض الحضور. ";

            }
            else
            {

                ViewData["Falied"] = "حدث خطأ أثناء معالجتك طلبك الرجاء المحاولة في وقت لاحق";

            }



            var attendT = attendx.FirstOrDefault();

            var domaininfo = await _AttendanceDomain.GetAllAttendanceBySeesionId(attendT.SessionId);


            return View(domaininfo);


        }
        
        public async Task<IActionResult> ExportAttendanceToExcel(Guid? id)
        {
            var attendanceList = await _AttendanceDomain.GetAllAttendanceBySeesionGuid(id);

            if (attendanceList == null || !attendanceList.Any())
            {
                return NotFound();
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("الحضور");
                worksheet.Cells["A1"].Value = "اسم الطالب";
                worksheet.Cells["B1"].Value = "حالة الحضور";
                worksheet.Cells["C1"].Value = "اسم الجلسة";
                int row = 2;
                foreach (var attendance in attendanceList)
                {
                    worksheet.Cells[$"A{row}"].Value = attendance.FullNameAr;
                    worksheet.Cells[$"B{row}"].Value = attendance.IsAttend ? "حاضر" : "غايب";
                    worksheet.Cells[$"C{row}"].Value = attendance.SessionNameAr;
                    row++;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string fileName = $"سجل الحضور{id}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        //public async Task<IActionResult> Details(int id)
        //{
        //    var attendance = await _AttendanceDomain.GetAttendanceById(id);
        //    if (attendance == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(attendance); 
        //}

    }
}
