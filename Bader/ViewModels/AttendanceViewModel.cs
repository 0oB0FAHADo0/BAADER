
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
        [DisplayName("Name")]
        [StringLength(100)]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الرمز")]


        public int SessionId { get; set; }

        [DisplayName("اسم المستخدم")]

        public string UserName { get; set; }

        [DisplayName("تاريخ الجلسه")]
        public DateTime SessionDate { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();

        [DisplayName("حاضر؟")]

        public bool IsAttend { get; set; }

        public string CourseNameAr { get; set; }

        public string SessionNameAr { get; set; }
        [DisplayName("الكلية")]
        [StringLength(100)]
        public string CollegeNameAr { get; set; }

        [DisplayName("الاسم الكامل")]
        public string FullNameAr { get; set; }

        public int RegistrationId { get; set; }




    }
}
