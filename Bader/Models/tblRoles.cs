namespace Bader.Models
{
    public class tblRoles
    {
        public int Id { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleNameEn { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public ICollection<tblPermissions> Permissions { get; set; }
        public bool IsDeleted { get; set; }
    }
}
