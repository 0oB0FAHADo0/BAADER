namespace Bader.Models
{
    public class tblPermissions// mohammed almulhim( viewModel,controler,view, domain) (add , update, delete)(sweetAlert - domain - controllers-in every function)(try and catch)
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public tblRoles Role { get; set; }
        public int RoleId { get; set; }
        public tblColleges College { get; set; }
        public int CollegeId { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
