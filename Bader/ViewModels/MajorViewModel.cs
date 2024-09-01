using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bader.ViewModels
{
    public class MajorViewModel
    {
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم التخصص بالعربي")]
        [StringLength(100)]
        public String MajorNameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم التخصص بالإنجليزي")]
        [StringLength(100)]
        public String MajorNameEn { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الاسم المادة ")]
        public string CourseNameAr { get; set; }

        [DisplayName("اسم الكلية")]
        public string CollageNameAr { get; set; }
        public Guid GUID { get; set; }


        public bool IsDeleted { get; set; }
    }
}
