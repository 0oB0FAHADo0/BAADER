using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Bader.Models
{
    public class BaaderContext : DbContext
    {
        public BaaderContext()
        {
            
        }
        public BaaderContext(DbContextOptions<BaaderContext> options) : base(options)
        {
        }
        public DbSet<tblUsers> tblUsers { get; set; }
        public DbSet<tblAttendance> tblAttendance { get; set; }
        public DbSet<tblRoles> tblRoles { get; set; }
        public DbSet<tblMajors> tblMajors { get; set; }
        public DbSet<tblColleges> tblColleges { get; set; }
        public DbSet<tblPermissions> tblPermissions { get; set; }
        public DbSet<tblLevels> tblLevels { get; set; }
        public DbSet<tblCourses> tblCourses { get; set; }
        public DbSet<tblContents> tblContents { get; set; }
        public DbSet<tblSessionsState> tblSessionsState { get; set; }
        public DbSet<tblSessions> tblSessions { get; set; }
        public DbSet<tblRegistrationsState> tblRegistrationsState { get; set; }
        public DbSet<tblRegistrations> tblRegistrations { get; set; }
        public DbSet<tblPermissionsLogs> tblPermissionsLogs { get; set; }
        public DbSet<tblCoursesLogs> tblCoursesLogs { get; set; }
        public DbSet<tblContentsLogs> tblContentsLogs { get; set; }
        public DbSet<tblSessionsLogs> tblSessionsLogs { get; set; }
        public DbSet<tblRegistrationsLogs> tblRegistrationsLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DBCS"));
        }
    }
}
