﻿namespace Bader.Models
{
    public class tblCoursesLogs
    {
        public int Id { get; set; }
        public string CourseId { get; set; }
        public DateTime DateTime { get; set; }
        public string OperationType { get; set; }
        public string CreatedBy { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
