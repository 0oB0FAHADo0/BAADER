using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bader.ViewModels
{
    public class UserViewModel
    {

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رقم المعرف")]
        public int Id { get; set; }

        [Required(ErrorMessage ="هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("إسم المستخدم")]
        public string Username { get; set; }

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("الإسم بالعربي")]
        public string FullNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("الإسم بالإنجليزي")]
        public string FullNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("كلمة المرور")]
        public string Password { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("البريد الإلكتروني")]
        public string Email { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("رقم الجوال")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("نوع المستخدم")]
        public string Usertype { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("اسم الكلية بالعربي")]
        public string CollegeNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [StringLength(100)]
        [DisplayName("اسم الكلية بالانجليزي")]
        public string CollegeNameEn { get; set; }
        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DisplayName("رمز الكلية")]
        public string CollegeCode { get; set; }
        public string RoleNameEn { get; set; }

    }
}

