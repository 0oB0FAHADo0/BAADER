using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bader.Domain
{
    public class UserRegistrationDomain
    {
        private readonly BaaderContext _context;

        public UserRegistrationDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegistrationViewModel>> GetAllRegistrations()
        {
            try
            {
                return await _context.tblRegistrations
                    .Include(n => n.RegistrationState)
                    .Include(y => y.Session).Where(e=> e.Session.IsDeleted==false && e.RegistrationState.IsDeleted == false)
                    .Where(x => x.Session.RegStartDate <= DateTime.Now && x.Session.RegEndDate >= DateTime.Now && x.RegistrationStateId == 1)
                    .Select(x => new RegistrationViewModel
                    {
                        RegistrationStateId = x.RegistrationStateId,
                        SessionId = x.SessionId,
                        Username = x.Username,
                        FullNameAr = x.FullNameAr,
                        FullNameEn = x.FullNameEn,
                        Phone = x.Phone,
                        RegDate = x.RegDate,
                        GUID = x.GUID,
                        SessionNameAr = x.Session.SessionNameAr,
                        StateAr = x.RegistrationState.StateAr,
                        NumOfStudents = x.Session.NumOfStudents,
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<RegistrationViewModel>();

            }
        }

        public async Task<IEnumerable<RegistrationViewModel>> GetAllRegistrationsByUsername(string username)
        {
            try
            {
                return await _context.tblRegistrations
                    .Include(n => n.RegistrationState)
                    .Include(y => y.Session).Where(e => e.Session.IsDeleted == false && e.RegistrationState.IsDeleted == false)
                    .Where(x => x.Session.RegStartDate <= DateTime.Now && x.Session.RegEndDate >= DateTime.Now && x.RegistrationStateId == 1)
                    .Where(u => u.Username == username)
                    .Select(x => new RegistrationViewModel
                    {
                        RegistrationStateId = x.RegistrationStateId,
                        SessionId = x.SessionId,
                        Username = x.Username,
                        FullNameAr = x.FullNameAr,
                        FullNameEn = x.FullNameEn,
                        Phone = x.Phone,
                        RegDate = x.RegDate,
                        GUID = x.GUID,
                        SessionNameAr = x.Session.SessionNameAr,
                        StateAr = x.RegistrationState.StateAr,
                        NumOfStudents = x.Session.NumOfStudents,
                    })
                    .ToListAsync();
            }
            catch
            {
                return new List<RegistrationViewModel>();

            }
        }

        public async Task<IEnumerable<tblSessions>> GetSessionsOld()
        {
            return await _context.tblSessions.Include(e=> e.Course).Include(r=> r.SessionState).Where(w => w.SessionState.IsDeleted == false && w.Course.IsDeleted==false)
                .Where(u => u.IsDeleted == false && u.RegStartDate <= DateTime.Now && u.RegEndDate >= DateTime.Now).ToListAsync();
        }

        public int GetCollegeIdByCollegeCode(string CollegeCode)
        {
            try
            {
                var College = _context.tblColleges.Where(u => u.IsDeleted == false).AsNoTracking().FirstOrDefault(tblColleges => tblColleges.CollegeCode == CollegeCode);
                return College.Id;
            }
            catch
            {
                return 0;
            }
        }
        public async Task<IEnumerable<SessionsViewModel>> GetSessions(string CollegeCode)
        {
            try
            {
                int collegeId = GetCollegeIdByCollegeCode(CollegeCode);

                return await _context.tblSessions.Include(c => c.SessionState).Include(x => x.Course).Where(w => w.SessionState.IsDeleted == false && w.Course.IsDeleted == false)
                    .Where(u => u.IsDeleted == false && u.RegStartDate <= DateTime.Now && u.RegEndDate >= DateTime.Now && u.Course.College.CollegeCode == CollegeCode).Select(x => new SessionsViewModel
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
            catch
            {
                return new List<SessionsViewModel>();

            }

        }
        public async Task<SessionsViewModel> GetSessionsById(Guid guid)
        {
            try
            {
                tblSessions session = _context.tblSessions.Where(u => u.IsDeleted == false).Include(e => e.Course).AsNoTracking().FirstOrDefault(tblSessions => tblSessions.GUID == guid);
                SessionsViewModel sessionsx = new SessionsViewModel();
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
                sessionsx.CourseNameAr = session.Course.CourseNameAr;
                return sessionsx;
            }
            catch
            {
                return null;

            }
        }

        public async Task<SessionsViewModel> GetSessionByIdNotGuid(int id)
        {
            try
            {
                tblSessions session = await _context.tblSessions.Include(e => e.Course).FirstOrDefaultAsync(e => e.Id == id);

                SessionsViewModel sessionsx = new SessionsViewModel();
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
                sessionsx.CourseNameAr = session.Course.CourseNameAr;

                return sessionsx;
            }
            catch
            {
                return null;

            }

        }

        public int GetSessionsIdByGUId(Guid guid)
        {
            try
            {
                var seesion = _context.tblSessions.Where(u => u.IsDeleted == false).AsNoTracking().FirstOrDefault(tblSessions => tblSessions.GUID == guid);
                return seesion.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> AddRegistration(RegistrationViewModel Reg, String username)
        {
            try
            {
                tblRegistrations Regx = new tblRegistrations();


                Regx.SessionId = Reg.SessionId;
                Regx.Username = Reg.Username;
                Regx.FullNameAr = Reg.FullNameAr;
                Regx.FullNameEn = Reg.FullNameEn;
                Regx.Phone = Reg.Phone;
                Regx.RegDate = DateTime.Now;

                Regx.GUID = Guid.NewGuid();
                Regx.RegistrationStateId = 1;


                _context.tblRegistrations.Add(Regx);

                _context.SaveChanges();

                tblRegistrationsLogs log = new tblRegistrationsLogs();
                log.RegistrationId = Regx.Id;
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.OperationType = "Add";
                log.RegistrationState = "مقبول";
                log.AdditionalInfo = "تم تقديم طلب عن طريق هذا المستخدم";
                _context.tblRegistrationsLogs.Add(log);


                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> DidUserRegBefore(String username, int SessionId)
        {

            return await _context.tblRegistrations.Where(u => u.SessionId == SessionId).AnyAsync(u => u.Username == username && u.RegistrationStateId == 1);

        }
        public async Task<bool> CheckForreActiveInCreate(String username, int SessionId)
        {

            return await _context.tblRegistrations.Where(u => u.SessionId == SessionId).AnyAsync(u => u.Username == username && u.RegistrationStateId == 2);

        }
        public async Task<RegistrationViewModel> GetRegByGuid(Guid gudi)
        {
            try
            {
                var reg = _context.tblRegistrations.AsNoTracking().FirstOrDefault(tblRegistrations => tblRegistrations.GUID == gudi);


                RegistrationViewModel regx = new RegistrationViewModel();

                regx.SessionId = reg.SessionId;
                regx.Username = reg.Username;
                regx.FullNameAr = reg.FullNameAr;
                regx.FullNameEn = reg.FullNameEn;
                regx.Phone = reg.Phone;
                regx.RegDate = reg.RegDate;

                regx.GUID = reg.GUID;
                regx.RegistrationStateId = reg.RegistrationStateId;

                return regx;
            }
            catch
            {
                return null;

            }
        }

        public Guid GetGuidByUsername(string username)
        {

            var Reg = _context.tblRegistrations.AsNoTracking().FirstOrDefault(tblRegistrations => tblRegistrations.Username == username);

            return Reg.GUID;
        }
        public int GetRegIdByGuid(Guid gudi)
        {

            var Reg = _context.tblRegistrations.AsNoTracking().FirstOrDefault(tblRegistrations => tblRegistrations.GUID == gudi);

            return Reg.Id;
        }
        public async Task<int> UpdateRegistration(RegistrationViewModel Reg, String username)
        {

            try
            {
                tblRegistrations Regx = new tblRegistrations();


                Regx.Id = GetRegIdByGuid(Reg.GUID);

                Regx.SessionId = Reg.SessionId;
                Regx.Username = Reg.Username;
                Regx.FullNameAr = Reg.FullNameAr;
                Regx.FullNameEn = Reg.FullNameEn;
                Regx.Phone = Reg.Phone;

                Regx.RegistrationStateId = Reg.RegistrationStateId;
                Regx.RegDate = Reg.RegDate;
                _context.tblRegistrations.Update(Regx);

                _context.SaveChanges();

                tblRegistrationsLogs log = new tblRegistrationsLogs();
                log.RegistrationId = Regx.Id;
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.OperationType = "Update";
                log.RegistrationState = "مقبول";
                log.AdditionalInfo = "تم تعديل طلب عن طريق هذا المستخدم";
                _context.tblRegistrationsLogs.Add(log);


                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }


        public async Task<int> GetNumOfSutdentById(int id)
        {
            var Session = await _context.tblSessions.FindAsync(id);


            return Session.NumOfStudents;
        }
        public async Task<int> CheckCountReg(int Id)
        {
            try
            {
                int NumOfSutdent = await GetNumOfSutdentById(Id);
                var sess = _context.tblRegistrationsState.AsNoTracking().FirstOrDefault(tblRegistrationsState => tblRegistrationsState.Id == 1);
                int currentRegistrationCount = await _context.tblRegistrations.Where(r => r.SessionId == Id && r.RegistrationStateId == sess.Id).CountAsync();
                int d = 3;
                if (currentRegistrationCount >= NumOfSutdent)
                {
                    return 0;
                }
                else if (currentRegistrationCount < NumOfSutdent)
                {
                    return 1;
                }
            }
            catch
            {

            }

            return 0;


        }



        public void UpdateSessionStates()
        {
            DateTime currentTime = DateTime.Now;

            // Query sessions where SessionDate, RegEndDate, and RegStartDate are all in the past
            var sessionsToUpdate = _context.tblSessions
                .Where(s => currentTime > s.SessionDate && currentTime > s.RegEndDate)
                .ToList();

            // Update the SessionStateId and SessionState for each session
            foreach (var session in sessionsToUpdate)
            {
                session.SessionStateId = 2;
            }

            // Save changes to the database
            _context.SaveChanges();
        }

    }
}
