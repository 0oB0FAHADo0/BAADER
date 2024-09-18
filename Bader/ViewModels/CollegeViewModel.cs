using Bader.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bader.ViewModels
{
    public class CollegeViewModel
    {

        
        

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الاسم")]
        [StringLength(100)]
        [RegularExpression(@"^[\u0600-\u06FF\s]+$", ErrorMessage = "الرجاء إدخال الاسم باللغة العربية فقط")]
        public string CollegeNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("Name")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "الرجاء إدخال الاسم باللغة الإنجليزية فقط")]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الرمز")]
        public string CollegeCode { get; set; }

        public bool IsDeleted { get; set; }
        public Guid GUID { get; set; }

        public bool? Gender { get; set; }


    }
}
