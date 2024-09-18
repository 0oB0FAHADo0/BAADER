using Bader.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bader.ViewModels
{
    public class CourseViewModel
    {
       
        //public int? Id { get; set; }

        [DisplayName("معرف المقرر")]
        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        public int CollegeId { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رمز الكلية")]
        public int CollegeCode { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف المستوى")]
        public int LevelId { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الرمز")]
        public string CourseNum { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الاسم")]
        public string CourseNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("Name")]
        public string CourseNameEn { get; set; }
       
        
        
        
        
        [DisplayName("المستوى")]
        public string LevelNameAr { get; set; }

        [DisplayName("الكلية")]
        public string CollageNameAr { get; set; }

        [DisplayName("التخصص")]
        public string MajorNameAr { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف التخصص")]
        public int MajorId { get; set; }

        public Guid GUID { get; set; }


        public bool IsDeleted { get; set; }
    }
}
