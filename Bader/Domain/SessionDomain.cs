using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bader.Domain
{
    public class SessionDomain
    {
        private readonly BaaderContext _context;

        public SessionDomain(BaaderContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SessionsViewModel>> GetSessions()
        {
            return await _context.tblSessions.Where(u => u.IsDeleted == false).Include(c => c.SessionState).Include(x=> x.Course).Select(x => new SessionsViewModel
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
        public async Task<IEnumerable<SessionsViewModel>> GetSomeSessions(String CollageCode)
        {
            try
            {
                return await _context.tblSessions.Include(x => x.Course.College).Include(c => c.SessionState).Include(x => x.Course).Where(x => x.IsDeleted == false && x.Course.College.CollegeCode == CollageCode && x.Course.College.IsDeleted == false ).Select(x => new SessionsViewModel

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
            catch (Exception ex)
            {

                return new List<SessionsViewModel>();

            }

        }
        public async Task<IEnumerable<tblSessionsState>> getFromSessionsState()
        {//
            return await _context.tblSessionsState.Where(u => u.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<tblCourses>> getFromtblCourses()
        {//
            return await _context.tblCourses.Include(u => u.College).Include(u => u.Major).Include(u => u.Level).Where(u => u.IsDeleted == false && u.College.IsDeleted == false && u.Major.IsDeleted == false && u.Level.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<tblCourses>> getSomeFromtblCourses(string collageCode)
        {
            try
            {
                return await _context.tblCourses.Include(x => x.College).Include(x => x.Major).Include(x => x.Level).Where(x => x.IsDeleted == false && x.College.CollegeCode == collageCode && x.College.IsDeleted == false && x.Major.IsDeleted == false && x.Level.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<tblCourses>();
            }
        }
        public async Task <int> AddSessions(SessionsViewModel Session , String username)
        {
            try
            {
                tblSessions Sessionx = new tblSessions();
                // Sessionx.Id = Session.Id;
                Sessionx.SessionStateId = Session.SessionStateId;
                Sessionx.SessionNameAr = Session.SessionNameAr;
                Sessionx.SessionNameEn = Session.SessionNameEn;
                Sessionx.Gender = Session.Gender;
                Sessionx.CourseId = Session.CourseId;
                Sessionx.TitleAr = Session.TitleAr;
                Sessionx.TitleEn = Session.TitleEn;
                Sessionx.Links = Session.Links;
                Sessionx.NumOfStudents = Session.NumOfStudents ?? 0;
                Sessionx.SessionDate = Session.SessionDate ?? default(DateTime);
                Sessionx.RegEndDate = Session.RegEndDate ?? default(DateTime);
                Sessionx.RegStartDate = Session.RegStartDate ?? default(DateTime);
                Sessionx.GUID = Guid.NewGuid();
                Sessionx.IsDeleted = Session.IsDeleted;
                _context.tblSessions.Add(Sessionx);

                _context.SaveChanges();
                
                    tblSessionsLogs log = new tblSessionsLogs();
                    log.SessionId = Sessionx.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Add";
                    log.CreatedBy = username;
                    log.AdditionalInfo = $"تم إضافة {Sessionx.SessionNameAr} عن طريق هذا المستخدم";
                    _context.tblSessionsLogs.Add(log);
                

                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }
        public async Task<int> AddSomeSessions(SessionsViewModel Session, String username,string CollageCode)
        {
            try
            {
                var college = await _context.tblColleges.Where(c => c.CollegeCode == CollageCode && c.IsDeleted == false).FirstOrDefaultAsync();
                if (college == null)
                {

                    return 0;
                }

                tblSessions Sessionx = new tblSessions();
                // Sessionx.Id = Session.Id;
                Sessionx.SessionStateId = Session.SessionStateId;
                Sessionx.SessionNameAr = Session.SessionNameAr;
                Sessionx.SessionNameEn = Session.SessionNameEn;
                Sessionx.Gender = Session.Gender;
                Sessionx.CourseId = Session.CourseId;
                Sessionx.TitleAr = Session.TitleAr;
                Sessionx.TitleEn = Session.TitleEn;
                Sessionx.Links = Session.Links;
                Sessionx.NumOfStudents = Session.NumOfStudents ?? 0;
                Sessionx.SessionDate = Session.SessionDate ?? default(DateTime);
                Sessionx.RegEndDate = Session.RegEndDate?? default(DateTime);
                Sessionx.RegStartDate = Session.RegStartDate ?? default(DateTime);
                Sessionx.GUID = Guid.NewGuid();
                Sessionx.IsDeleted = Session.IsDeleted;
                _context.tblSessions.Add(Sessionx);

                _context.SaveChanges();

                tblSessionsLogs log = new tblSessionsLogs();
                log.SessionId = Sessionx.Id;
                log.DateTime = DateTime.Now;
                log.OperationType = "Add";
                log.CreatedBy = username;
                log.AdditionalInfo = $"تم إضافة {Sessionx.SessionNameAr} عن طريق هذا المستخدم";
                _context.tblSessionsLogs.Add(log);


                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }
        public int GetSessionsByGUId(Guid guid)
        {
            var seesion = _context.tblSessions.Where(u => u.IsDeleted == false).AsNoTracking().FirstOrDefault(tblSessions=> tblSessions.GUID== guid);
            return seesion.Id;
        }
        public async Task <int> UpdateSessions(SessionsViewModel session, String username)
        {
            try
            {
                tblSessions sessionsx = new tblSessions();
                sessionsx.Id = GetSessionsByGUId(session.GUID);
                sessionsx.GUID = session.GUID;

                sessionsx.SessionNameAr = session.SessionNameAr;
                sessionsx.SessionNameEn = session.SessionNameEn;
                sessionsx.SessionStateId = session.SessionStateId;
                sessionsx.Gender = session.Gender;
                sessionsx.CourseId = session.CourseId;
                sessionsx.TitleAr = session.TitleAr;
                sessionsx.TitleEn = session.TitleEn;
                sessionsx.Links = session.Links;
                sessionsx.NumOfStudents = session.NumOfStudents ?? 0 ;
                sessionsx.SessionDate = session.SessionDate ?? default(DateTime);
                sessionsx.RegEndDate = session.RegEndDate ?? default(DateTime);
                sessionsx.RegStartDate = session.RegStartDate ?? default(DateTime);
                sessionsx.IsDeleted = session.IsDeleted;

                _context.tblSessions.Update(sessionsx);

                _context.SaveChanges();
                
                    tblSessionsLogs log = new tblSessionsLogs();
                    log.SessionId = sessionsx.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Update";
                    log.CreatedBy = username;
                    log.AdditionalInfo = $"تم تحديث {sessionsx.SessionNameAr} عن طريق هذا المستخدم";
                    _context.tblSessionsLogs.Add(log);
                

                int check = await _context.SaveChangesAsync();
                return check;
            }

            catch  {
                return 0;
            }

        }
        public async Task<SessionsViewModel> GetSessionsById(Guid guid)
        {
            var session = _context.tblSessions.Include(x => x.SessionState).Include(y => y.Course).Where(u => u.IsDeleted == false).AsNoTracking().FirstOrDefault(tblSessions => tblSessions.GUID == guid);
            SessionsViewModel sessionsx = new SessionsViewModel();
            sessionsx.GUID =  session.GUID;

            sessionsx.SessionNameAr = session.SessionNameAr;
            sessionsx.SessionNameEn = session.SessionNameEn;
            sessionsx.SessionStateId = session.SessionStateId;
            sessionsx.StateAr = session.SessionState.StateAr;
            sessionsx.CourseNameAr = session.Course.CourseNameAr;
            sessionsx.CourseId = session.CourseId; 
            sessionsx.TitleAr = session.TitleAr;
            sessionsx.TitleEn = session.TitleEn;
            sessionsx.Links = session.Links;
            sessionsx.NumOfStudents = session.NumOfStudents;
            sessionsx.Gender = session.Gender;
            sessionsx.SessionDate = session.SessionDate;
            sessionsx.RegEndDate = session.RegEndDate;
            sessionsx.RegStartDate = session.RegStartDate;
            return sessionsx;
        }

        public async Task<IEnumerable<SessionsViewModel>> GetSessionsdate()
        {
            return await _context.tblSessions.Select(x => new SessionsViewModel
            {
                
                SessionDate = x.SessionDate,
                
            }).ToListAsync();
        }

    }
}
