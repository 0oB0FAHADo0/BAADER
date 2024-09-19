using Bader.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bader.ViewModels
{
    public class ContentViewModel
    {
        public int? Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف المادة")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("العنوان")]
        public string TitleAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("Title")]
        public string TitleEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("المحتوى")]
        public string ContentsAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("Content")]
        public string ContentsEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(1000, MinimumLength = 10)]
        [DisplayName("الروابط")]
        public string Links { get; set; }
        [DisplayName("معرف(GUID) ")]
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }
        [DisplayName("الكلية")]
        public string CollageNameAr { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف الكلية")]
        public int CollegeId { get; set; }
        [DisplayName("التخصص")]
        public string MajorNameAr { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف التخصص")]
        public int MajorId { get; set; }
        [DisplayName("اسم المقرر")]
        public string CourseNameAr { get; set; }
    }
}
