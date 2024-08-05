namespace Bader.Models
{
    public class tblPermissions
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public tblRoles Role { get; set; }
        public int RoleId { get; set; }
        public tblColleges College { get; set; }
        public int? CollegeId { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
    }
}
