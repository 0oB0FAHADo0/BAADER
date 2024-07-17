namespace Bader.Models
{
    public class tblContents
    {
        public int Id { get; set; }
        public tblCourses Course { get; set; }
        public int CoursesId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string ContentsAr { get; set; }
        public string ContentsEn { get; set; }
        public string Links { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }


    }
}
