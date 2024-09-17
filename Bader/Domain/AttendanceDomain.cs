
using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bader.Domain
{
    public class AttendanceDomain
    {
        private readonly BaaderContext _context;

        public AttendanceDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttendanceViewModel>> GetAllAttendanceBySeesionGuid(Guid? guid)
        {
            try
            {

				var session = _context.tblSessions.AsNoTracking().FirstOrDefault(tblAttendance => tblAttendance.GUID == guid);
				var Attend= await _context.tblAttendance.Where(t=> t.Session.IsDeleted==false )
                    .Include(a => a.Session).Where(a=> a.SessionId == session.Id).Include(e=> e.Registration)
                    .Where(i => i.Registration.RegistrationStateId == 1)
                    .Select(a => new AttendanceViewModel
                    {
                        SessionId = a.SessionId,
                        SessionDate = a.SessionDate,
                        UserName = a.UserName,
                        IsAttend = a.IsAttend,
                        GUID = a.GUID,
                        SessionNameAr = a.Session.SessionNameAr,
                        RegistrationId= a.RegistrationId,
                    })
                    .ToListAsync();


                foreach (var user in Attend)
                {
                    var userinfo = _context.tblUsers.AsNoTracking().FirstOrDefault(tblUsers => tblUsers.Username == user.UserName);
                    user.FullNameAr = userinfo.FullNameAr;
                }


                return Attend;
            }
            catch
            {
                return new List<AttendanceViewModel>();
            }
        }

        public async Task<IEnumerable<SessionsViewModel>> GetSessions()
        {
            return await _context.tblSessions.Where(u => u.IsDeleted == false).Include(c => c.SessionState).Include(x => x.Course).Where(t => t.SessionState.IsDeleted == false && t.Course.IsDeleted == false)
                .Select(x => new SessionsViewModel
            {
                SessionStateId = x.SessionStateId,
                SessionNameAr = x.SessionNameAr,
                SessionNameEn = x.SessionNameEn,
                Gender = x.Gender,
                CourseId = x.CourseId,
                TitleAr = x.TitleAr,
                TitleEn = x.TitleEn,
                Links = x.Links,
                NumOfStudents = x.NumOfStudents,
                SessionDate = x.SessionDate,
                RegEndDate = x.RegEndDate,
                RegStartDate = x.RegStartDate,
                GUID = x.GUID,
                IsDeleted = x.IsDeleted,
                StateAr = x.SessionState.StateAr,
                CourseNameAr = x.Course.CourseNameAr,
                CollegeNameAr = x.Course.College.CollegeNameAr,
            }).ToListAsync();
        }
        public async Task<IEnumerable<SessionsViewModel>> GetSessionsByCollegeCode(string CollegeCode)
        {
            return await _context.tblSessions.Where(u => u.IsDeleted == false).Where(x=> x.Course.College.CollegeCode==CollegeCode).Include(c => c.SessionState).Include(x => x.Course).Where(t => t.SessionState.IsDeleted == false && t.Course.IsDeleted == false)
                .Select(x => new SessionsViewModel
            {
                SessionStateId = x.SessionStateId,
                SessionNameAr = x.SessionNameAr,
                SessionNameEn = x.SessionNameEn,
                Gender = x.Gender,
                CourseId = x.CourseId,
                TitleAr = x.TitleAr,
                TitleEn = x.TitleEn,
                Links = x.Links,
                NumOfStudents = x.NumOfStudents,
                SessionDate = x.SessionDate,
                RegEndDate = x.RegEndDate,
                RegStartDate = x.RegStartDate,
                GUID = x.GUID,
                IsDeleted = x.IsDeleted,
                StateAr = x.SessionState.StateAr,
                CourseNameAr = x.Course.CourseNameAr,
            }).ToListAsync();
        }



        public async Task<IEnumerable<AttendanceViewModel>> GetAllAttendanceBySeesionId(int id)
        {
            try
            {

                return await _context.tblAttendance
                    .Include(a => a.Session).Where(a => a.SessionId == id).Where(i => i.Registration.RegistrationStateId == 1)
                    .Where(e=> e.Session.IsDeleted==false)
                    .Select(a => new AttendanceViewModel
                    {
                        SessionId = a.SessionId,
                        SessionDate = a.SessionDate,
                        UserName = a.UserName,
                        IsAttend = a.IsAttend,
                        GUID = a.GUID,
                        SessionNameAr = a.Session.SessionNameAr,
                        RegistrationId = a.RegistrationId,
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<AttendanceViewModel>();
            }
        }

        // Get attendance by ID
        //public async Task<AttendanceViewModel> GetAttendanceById(int id)
        //{
        //    try
        //    {
        //        return await _context.tblAttendance
        //            .Include(a => a.Session)
        //            .Where(a => a.Id == id)
        //            .Select(a => new AttendanceViewModel
        //            {
        //                Id = a.Id,
        //                SessionId = a.SessionId,
        //                SessionDate = a.SessionDate,
        //                UserName = a.UserName,
        //                IsAttend = a.IsAttend,
        //                GUID = a.GUID,
        //                SessionNameAr = a.Session.SessionNameAr // Ensure this property is included
        //            })
        //            .FirstOrDefaultAsync();
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}


        public async Task<int> addStudentInAteend(AttendanceViewModel attend) 
        {
            try
            {
                tblAttendance attendx =  new tblAttendance();


                attendx.SessionId = attend.SessionId;
                attendx.UserName = attend.UserName;
                attend.SessionDate = attend.SessionDate;
				attendx.IsAttend = false;
                attendx.RegistrationId = attend.RegistrationId;


                _context.tblAttendance.Add(attendx);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }
   

        }

        public int GetattendIdByGuid(Guid gudi)
        {

            var attend = _context.tblAttendance.AsNoTracking().FirstOrDefault(tblAttendance => tblAttendance.GUID == gudi);

            return attend.Id;



        }
        public async Task<AttendanceViewModel> GetattendByGuid(Guid gudi)
        {

            var attendViewModel = await _context.tblAttendance
           .AsNoTracking().Where(tblAttendance => tblAttendance.GUID == gudi).Select(attendEntity => new AttendanceViewModel
           {
            UserName = attendEntity.UserName,
            SessionDate = attendEntity.SessionDate,
            IsAttend = attendEntity.IsAttend,
            GUID = attendEntity.GUID,
            SessionId = attendEntity.SessionId,
            RegistrationId=attendEntity.RegistrationId,

           }).FirstOrDefaultAsync();

            return attendViewModel; 




        }

        public async Task<int> updateAttend(Guid guid,bool IsAttend)
        {
            
            try
            {

                
                tblAttendance attendx = new tblAttendance();

                AttendanceViewModel attend = await GetattendByGuid(guid);

                attendx.IsAttend = IsAttend;

                attendx.Id = GetattendIdByGuid(guid);
                attendx.GUID = guid;

                attendx.SessionDate = attend.SessionDate;
                attendx.SessionId = attend.SessionId;
                attendx.UserName = attend.UserName;
                attendx.RegistrationId = attend.RegistrationId;
                




                _context.tblAttendance.Update(attendx);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }
    }
}
