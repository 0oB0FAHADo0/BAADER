
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using Bader.Models;

namespace Bader.ViewModels
{
    public class SessionsViewModel
    {

        //star
       // public int? Id { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف حالة الجلسة")]
        //public tblSessionsState SessionState { get; set; }
        public int SessionStateId { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("الاسم")]
        public string SessionNameAr { get; set; }
        //end


        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("Name")]
        public string SessionNameEn { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف الدورة")]
        //  public tblCourses Course { get; set; }
        public int CourseId { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("العنوان ")]
        public string TitleAr { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("Title")]
        public string TitleEn { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Url(ErrorMessage = "الرابط غير صالح")]
        [StringLength(200)]
        [DisplayName("الروابط")]
        public string Links { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب أن يكون عدد الطلاب أكبر من 0")]
        [DisplayName("عدد الطلاب")]
        public int NumOfStudents { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("تاريخ الجلسة")]
        [DataType(DataType.DateTime)]

        public DateTime SessionDate { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("تاريخ نهاية التسجيل")]
        [DataType(DataType.DateTime)]

        public DateTime RegEndDate { get; set; }
        //end

        //star
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("تاريخ بداية التسجيل")]
        [DataType(DataType.DateTime)]
        public DateTime RegStartDate { get; set; }
        //end

        //star
       // [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الجنس")]
        public bool? Gender { get; set; }
        //end

        //star

        [DisplayName("الحالة")]
        public string StateAr { get; set; }
        //end
        [DisplayName("اسم المقرر")]
        public string CourseNameAr { get; set; }
        //end

        //star

        [DisplayName("معرف (Guid)")]
        public Guid GUID { get; set; }
        //end

        public bool IsDeleted { get; set; }

        [DisplayName("التخصص")]
        [StringLength(100)]
        public String MajorNameAr { get; set; }


        [DisplayName("الكلية")]
        [StringLength(100)]
        public string CollegeNameAr { get; set; }

    }
}
