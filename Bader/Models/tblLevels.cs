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
<<<<<<< HEAD
        public bool IsDeleted { get; set; }
=======
        public bool IsDeleted { get; set; } = false;
>>>>>>> 595baa6ce19721790143a9494bd5e955c74f6b0a
    }
}
