namespace Bader.Models
{
    public class tblPermissions// mohammed almulhim( viewModel,controler,view, domain) (add , update, delete)
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public tblRoles Role { get; set; }
        public int RoleId { get; set; }
        public tblColleges College { get; set; }
        public int? CollegeId { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
<<<<<<< HEAD
        public bool IsDeleted { get; set; }
=======
        public bool IsDeleted { get; set; } = false;
>>>>>>> 595baa6ce19721790143a9494bd5e955c74f6b0a
    }
}
