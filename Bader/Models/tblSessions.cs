namespace Bader.Models
{
    public class tblSessions// abdulmohsin ( viewModel,controler,view, domain) (add , update, delete)
    {
        public int Id { get; set; }
        public tblSessionsState SessionState { get; set; }
        public int SessionStateId { get; set; }
        public string SessionNameAr { get; set; }
        public string SessionNameEn { get; set; }
        public tblCourses Course { get; set; }
        public int CourseId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Links { get; set; }
        public int NumOfStudents { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime RegEndDate { get; set; }
        public DateTime RegStartDate { get; set; }
        public Guid GUID { get; set; }
        public bool IsDeleted { get; set; }

    }
}
