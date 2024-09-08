
using Bader.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bader.ViewModels
{
    public class AttendenceViewModel
    {
        public int Id { get; set; }
        public tblSessions Session { get; set; }

        public int SessionId { get; set; }

        public string UserName { get; set; }

        public DateTime SessionDate { get; set; }
        public Guid GUID { get; set; } = Guid.NewGuid();
        public bool IsAttend { get; set; }

    }
}
