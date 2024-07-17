namespace Bader.Models
{
    public class tblContentsLogs
    {
        public int Id { get; set; }
        public int ContentID { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
