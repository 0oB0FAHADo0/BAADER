namespace Bader.Models
{
    public class tblAttendence// mohammmed alsubai ( viewModel,controler,view, domain) (add , update, delete)
    {
        public int Id { get; set; }
        public tblSessions Sesiion { get; set; }

        public int SessionId { get; set; }

        public string UserName { get; set; }

        public int SessionDate { get; set; }


        public string IsAttend { get; set; }



    }
}
