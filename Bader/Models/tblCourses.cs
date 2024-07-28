namespace Bader.Models
{
    public class tblCourses //ali ( viewModel,controler,view, domain) (add , update, delete)
    {
        public int Id { get; set; }
        public tblColleges College { get; set; }
        public int CollegeId { get; set; }
        public tblLevels Level { get; set; }
        public int LevelId { get; set; }
        public int CourseNum { get; set; } 
        public string CourseNameAr { get; set; }
        public string CourseNameEn { get; set; }
        public ICollection<tblSessions> Sessions { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }



    }
}
