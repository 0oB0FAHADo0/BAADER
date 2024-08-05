namespace Bader.Models
{
    public class tblRegistrationsState
    {
        public int Id { get; set; }
        public string StateAr { get; set; }
        public string StateEn { get; set; }
        public ICollection<tblRegistrations> Registrations { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

    }
}
