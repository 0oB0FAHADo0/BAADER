namespace Bader.Models
{
    public class tblSessionsState
    {
        public int Id { get; set; }
        public string StateAr { get; set; }
        public string StateEn { get; set; }
        public ICollection<tblSessions> Sessions { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }

    }
}
