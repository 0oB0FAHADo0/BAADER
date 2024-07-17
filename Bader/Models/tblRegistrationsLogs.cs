namespace Bader.Models
{
    public class tblRegistrationsLogs
    {
        public int Id { get; set; }
        public int RegistrationId { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string AdditionalInfo { get; set; }
        public string RegistrationState { get; set; }
    }
}
