using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bader.ViewModels
{
    public class MajorViewModel
    {
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الإسم ")]
        [StringLength(100)]
        public String MajorNameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("name")]
        [StringLength(100)]
        public String MajorNameEn { get; set; }

        //[Required(ErrorMessage = "هذا الحقل إجباري")]
        //[DisplayName("الاسم المادة ")]
        //public string CourseNameAr { get; set; }

        [DisplayName(" الكلية")]
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [StringLength(100)]
        public string CollageNameAr { get; set; }
        public Guid GUID { get; set; }


        public bool IsDeleted { get; set; }
    }
}
