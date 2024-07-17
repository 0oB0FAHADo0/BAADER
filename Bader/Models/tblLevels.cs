namespace Bader.Models
{
    public class tblLevels
    {
        public int Id { get; set; }
        public string LevelNameAr { get; set; }
        public string LevelNameEn { get; set; }
        public int LevelNum { get; set; }
        public ICollection<tblCourses> Courses { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
