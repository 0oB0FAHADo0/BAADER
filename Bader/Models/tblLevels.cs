namespace Bader.Models
{
    public class tblLevels // rakan ( viewModel,controler,view, domain) (add , update, delete) 
    {
        public int Id { get; set; }
        public string LevelNameAr { get; set; }
        public string LevelNameEn { get; set; }
        public int LevelNum { get; set; }
        public ICollection<tblCourses> Courses { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
    }
}
