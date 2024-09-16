using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Bader.Models;

namespace Bader.ViewModels
{
    public class PermissionViewModel
    {

        //[Required(ErrorMessage = "هذا الحقل إجباري")]
        //[DisplayName("رقم المعرف")]
        //public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("إسم المستخدم")]
        public string Username { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }



        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رمز الصلاحية")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رقم الكلية")]
        public int CollegeId { get; set; }


        [DisplayName("Guid")]
        public Guid GUID { get; set; }

        [DisplayName("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
