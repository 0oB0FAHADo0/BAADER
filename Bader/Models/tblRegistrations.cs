namespace Bader.Models
{
    public class tblRegistrations
    {
        public int Id { get; set; }
        public tblRegistrationsState RegistrationState { get; set; }
        public int RegistrationStateId { get; set; }
        public tblSessions Session { get; set; }
        public int SessionId { get; set; }
        public string Username { get; set; }
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public DateTime RegDate { get; set; }
        public string Phone { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();

        public string Email { get; set; }
    }
}
