namespace Bader.Models
{
    public class tblPermissionsLogs
    {
        public int Id { get; set; }
        public int PermissionId { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string Createdto { get; set; }
        public string PermissionType { get; set; }
    }
}
