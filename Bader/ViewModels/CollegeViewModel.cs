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
        public string CollegeNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("Name")]
        [StringLength(100)]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الرمز")]
        public string CollegeCode { get; set; }

        public bool IsDeleted { get; set; }
        public Guid GUID { get; set; }

        public bool? Gender { get; set; }


    }
}
