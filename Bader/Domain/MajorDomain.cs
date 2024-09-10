using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bader.Domain
{
    public class MajorDomain
    {
        private readonly BaaderContext _context;
        public MajorDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MajorViewModel>> GetMajors()
        {
            try
            {
                return await _context.tblMajors.Include(x => x.College).Where(x => x.IsDeleted == false).Select(x => new MajorViewModel
                {
                    MajorNameEn = x.MajorNameEn,
                    MajorNameAr = x.MajorNameAr,
                    CollageNameAr = x.College.CollegeNameAr,
                    GUID = x.GUID,

                }).ToListAsync();
            }
            catch (Exception ex)
            {

                return new List<MajorViewModel>();

            }

        }
        public async Task<IEnumerable<tblColleges>> GetCollages()
        {
            try
            {
                return await _context.tblColleges.Where(u => u.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<tblColleges>();
            }

        }
        public async Task<int> addMajors(MajorViewModel Major)
        {
            try
            {
                var college = await _context.tblColleges.Where(c =>c.IsDeleted == false).FirstOrDefaultAsync();

                if (college == null)
                {

                    return 0;
                }

                tblMajors Majorx = new tblMajors();
                Majorx.MajorNameAr = Major.MajorNameAr;
                Majorx.MajorNameEn = Major.MajorNameEn;
                Majorx.CollegeId = college.Id;
                Majorx.GUID = Guid.NewGuid();


                _context.tblMajors.Add(Majorx);

                _context.SaveChanges();

                //tblCoursesLogs log = new tblCoursesLogs();
                //log.CourseId = course.Id;
                //log.OperationType = "Add";
                //log.DateTime = DateTime.Now;
                //log.CreatedBy = username;
                //log.AdditionalInfo = $"تم إضافة مقرر {course.CourseNameAr} بواسطة هذا المستخدم";
                //_context.tblCoursesLogs.Add(log);


                int check = await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

    }
}
