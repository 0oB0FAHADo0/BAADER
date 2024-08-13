using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bader.ViewModels
{
    public class LevelViewModel
    {

        [Required(ErrorMessage = "هذا الحقل إجباري")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("المعرف")]
        public int Id { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [StringLength(100)]
        [DisplayName("اسم المستوى بالعربي")]
        public string LevelNameAr { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [StringLength(100)]
        [DisplayName("اسم المستوى بالانجليزي")]
        public string LevelNameEn { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [Range(1, 100, ErrorMessage = "رقم المستوى يجب أن يكون بين 1 و 100")]
        [DisplayName("رقم المستوي")]
        public int LevelNum { get; set; }

        public Guid GUID { get; set; }

        public bool IsDeleted { get; set; }
    }
}
