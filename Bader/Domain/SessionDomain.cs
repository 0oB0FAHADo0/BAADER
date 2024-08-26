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
        public async Task<IEnumerable<tblSessionsState>> getFromSessionsState()
        {//
            return await _context.tblSessionsState.Where(u => u.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<tblCourses>> getFromtblCourses()
        {//
            return await _context.tblCourses.Where(u => u.IsDeleted == false).ToListAsync();
        }
        public async Task <int> AddSessions(SessionsViewModel Session)
        {
            try
            {
                tblSessions Sessionx = new tblSessions();
                // Sessionx.Id = Session.Id;
                Sessionx.SessionStateId = Session.SessionStateId;
                Sessionx.SessionNameAr = Session.SessionNameAr;
                Sessionx.SessionNameEn = Session.SessionNameEn;
                Sessionx.CourseId = Session.CourseId;
                Sessionx.TitleAr = Session.TitleAr;
                Sessionx.TitleEn = Session.TitleEn;
                Sessionx.Links = Session.Links;
                Sessionx.NumOfStudents = Session.NumOfStudents;
                Sessionx.SessionDate = Session.SessionDate;
                Sessionx.RegEndDate = Session.RegEndDate;
                Sessionx.RegStartDate = Session.RegStartDate;
                Sessionx.GUID = Guid.NewGuid();
                Sessionx.IsDeleted = Session.IsDeleted;
                _context.tblSessions.Add(Sessionx);

                _context.SaveChanges();
                
                    tblSessionsLogs log = new tblSessionsLogs();
                    log.SessionId = Sessionx.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Add";
                    log.CreatedBy = "Abdullmahsen";
                    log.AdditionalInfo = "تم إضافة جلسة عن طريق هذا المستخدم";
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
        public async Task <int> UpdateSessions(SessionsViewModel session)
        {
            try
            {
                tblSessions sessionsx = new tblSessions();
                sessionsx.Id = GetSessionsByGUId(session.GUID);
                sessionsx.GUID = session.GUID;

                sessionsx.SessionNameAr = session.SessionNameAr;
                sessionsx.SessionNameEn = session.SessionNameEn;
                sessionsx.SessionStateId = session.SessionStateId;
                sessionsx.CourseId = session.CourseId;
                sessionsx.TitleAr = session.TitleAr;
                sessionsx.TitleEn = session.TitleEn;
                sessionsx.Links = session.Links;
                sessionsx.NumOfStudents = session.NumOfStudents;
                sessionsx.SessionDate = session.SessionDate;
                sessionsx.RegEndDate = session.RegEndDate;
                sessionsx.RegStartDate = session.RegStartDate;
                sessionsx.IsDeleted = session.IsDeleted;

                _context.tblSessions.Update(sessionsx);

                _context.SaveChanges();
                
                    tblSessionsLogs log = new tblSessionsLogs();
                    log.SessionId = sessionsx.Id;
                    log.DateTime = DateTime.Now;
                    log.OperationType = "Update";
                    log.CreatedBy = "Abdullmahsen";
                    log.AdditionalInfo = "تم تحديث جلسة عن طريق هذا المستخدم";
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
            var session = _context.tblSessions.Where(u => u.IsDeleted == false).AsNoTracking().FirstOrDefault(tblSessions => tblSessions.GUID == guid);
            SessionsViewModel sessionsx = new SessionsViewModel();
            sessionsx.GUID =  session.GUID;

            sessionsx.SessionNameAr = session.SessionNameAr;
            sessionsx.SessionNameEn = session.SessionNameEn;
            sessionsx.SessionStateId = session.SessionStateId;
            sessionsx.CourseId = session.CourseId;
            sessionsx.TitleAr = session.TitleAr;
            sessionsx.TitleEn = session.TitleEn;
            sessionsx.Links = session.Links;
            sessionsx.NumOfStudents = session.NumOfStudents;
            sessionsx.SessionDate = session.SessionDate;
            sessionsx.RegEndDate = session.RegEndDate;
            sessionsx.RegStartDate = session.RegStartDate;
            return sessionsx;
        }
      
    }
}
