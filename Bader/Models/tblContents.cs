namespace Bader.Models
{
    public class tblContents// mohammmed alsubai ( viewModel,controler,view, domain) (add , update, delete)
    {
        public int Id { get; set; }
        public tblCourses Course { get; set; }
        public int CourseId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string ContentsAr { get; set; }
        public string ContentsEn { get; set; }
        public string Links { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
<<<<<<< HEAD
        public bool IsDeleted { get; set; }
=======
        public bool IsDeleted { get; set; } = false;
>>>>>>> 595baa6ce19721790143a9494bd5e955c74f6b0a


    }
}
