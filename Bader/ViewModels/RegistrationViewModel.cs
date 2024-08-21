﻿using Bader.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bader.ViewModels
{
    public class RegistrationViewModel
    {
        //public int Id { get; set; }


        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("حالة التسجيل")]
        public int RegistrationStateId { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم الجلسة")]
        public int SessionId { get; set; }


        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("اسم المستخدم")]
        public string Username { get; set; }
        [DisplayName("الاسم بالعربي")]
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        public string FullNameAr { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("الاسم بالانجليزي")]
        public string FullNameEn { get; set; }

        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("تاريخ التسجيل")]
        public DateTime RegDate { get; set; }
        [Required(ErrorMessage = "هذا الحقل اجباري")]
        [DisplayName("رقم الجوال")]
        public string Phone { get; set; }
        public Guid GUID { get; set; }


        [DisplayName("اسم الجلسة")]
        public string SessionNameAr { get; set; }

        [DisplayName("حالة التسجيل")]
        public string StateAr { get; set; }

        [DisplayName("السعة الطلابية للجلسة")]
        public int NumOfStudents { get; set; }



    }

}
