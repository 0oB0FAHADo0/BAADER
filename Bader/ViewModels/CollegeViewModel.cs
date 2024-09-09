using Bader.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bader.ViewModels
{
    public class CollegeViewModel
    {

        
        

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم الكلية بالعربي")]
        [StringLength(100)]
        public string CollegeNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم الكلية بالانجليزي")]
        [StringLength(100)]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("رمز الكلية")]
        public string CollegeCode { get; set; }

        public bool IsDeleted { get; set; }
        public Guid GUID { get; set; }

        public bool? Gender { get; set; }

    }
}
