
using Bader.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bader.ViewModels
{
    public class AttendanceViewModel
    {

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الاسم")]
        [StringLength(100)]
        public string CollegeNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("Name")]
        [StringLength(100)]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الرمز")]

        public int Id { get; set; }
        public tblSessions Session { get; set; }

        public int SessionId { get; set; }

        public string UserName { get; set; }

        public DateTime SessionDate { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsAttend { get; set; }

        public string CourseNameAr { get; set; }

        public string SessionNameAr { get; set; }


 

    }
}
