namespace Bader.Models
{
    public class tblSessionsLogs
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
