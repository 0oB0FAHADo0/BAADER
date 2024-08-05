namespace Bader.Models
{
    public class tblSessionsState
    {
        public int Id { get; set; }
        public string StateAr { get; set; }
        public string StateEn { get; set; }
        public ICollection<tblSessions> Sessions { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
<<<<<<< HEAD
        public bool IsDeleted { get; set; }
=======
        public bool IsDeleted { get; set; } = false;
>>>>>>> 595baa6ce19721790143a9494bd5e955c74f6b0a

    }
}
