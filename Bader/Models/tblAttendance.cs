namespace Bader.Models
{
    public class tblAttendance
    {
        public int Id { get; set; }
        public tblSessions Session { get; set; }

        public int SessionId { get; set; }

        public string UserName { get; set; }

        public DateTime SessionDate { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsAttend { get; set; }



    }
}
