namespace Bader.Models
{
    public class tblColleges
    {
        public int Id { get; set; }
        public string CollegeNameAr { get; set; }
        public string CollegeNameEn { get; set; }
        public int CollegeCode { get; set; }
        public int BuildingNum { get; set; }
        public ICollection<tblPermissions> Permissions { get; set; }    
        public ICollection<tblCourses> Courses { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; }// test
    }
}
