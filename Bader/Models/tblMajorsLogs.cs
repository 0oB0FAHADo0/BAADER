namespace Bader.Models
{
    public class tblMajorsLogs
    {
        public int Id { get; set; }
        public int MajorsId { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
