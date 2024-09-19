namespace Bader.Models
{
    public class tblAttendanceLogs
    {
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string Createdto { get; set; }
        public string AdditionalInfo { get; set; }

    }
}
