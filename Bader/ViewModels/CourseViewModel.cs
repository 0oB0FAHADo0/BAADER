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

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف المقرر")]
        public int CollegeId { get; set; }
        
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("معرف المستوى")]
        public int LevelId { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رقم المقرر")]
        public string CourseNum { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الاسم بالعربي")]
        public string CourseNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("الاسم باللإنجليزي")]
        public string CourseNameEn { get; set; }
       
        
        
        
        
        [DisplayName("مستوى المقرر")]
        public string LevelNameAr { get; set; }

        [DisplayName("أسم الكلية")]
        public string CollageNameAr { get; set; }

        public Guid GUID { get; set; }


        public bool IsDeleted { get; set; }
    }
}
