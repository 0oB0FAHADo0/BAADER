namespace Bader.Models
{
    public class tblMajors
    {
        public int Id { get; set; }
        public string MajorNameAr { get; set; }
        public string MajorNameEn { get; set; }
        public int CollegeId { get; set; }
        public tblColleges College { get; set; }
        public ICollection<tblCourses> Courses { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;
    }
}
