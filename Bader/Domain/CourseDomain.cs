using Bader.Models;
using Bader.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Bader.Domain
{
    public class CourseDomain
    {
        private readonly BaaderContext _context;

        public CourseDomain(BaaderContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CourseViewModel>> GetCourses()
        {
            return await _context.tblCourses.Include(x => x.College).Include( x => x.Level).Where(x => x.IsDeleted == false).Select(x => new CourseViewModel
            {
                CourseNum = x.CourseNum,
                CourseNameAr = x.CourseNameAr,
                CourseNameEn = x.CourseNameEn,
                CollegeId = x.CollegeId,
                LevelId = x.LevelId,
                CollageNameAr = x.College.CollegeNameAr,
                LevelNameAr = x.Level.LevelNameAr,
                GUID = x.GUID,
                
            }).ToListAsync();
        }

        public async Task<IEnumerable<CourseViewModel>> GetSomeCourses()
        {
            int UserId = 9;
            return await _context.tblCourses.Include(x => x.Level).Include(x => x.College).Where(x => x.IsDeleted == false).Where(x => x.CollegeId == UserId).Select(x => new CourseViewModel
            {
                CourseNum = x.CourseNum,
                CourseNameAr = x.CourseNameAr,
                CourseNameEn = x.CourseNameEn,
                CollegeId = x.CollegeId,
                LevelId = x.LevelId,
                CollageNameAr = x.College.CollegeNameAr,
                LevelNameAr = x.Level.LevelNameAr,
                GUID = x.GUID,

            }).ToListAsync();
        }


        public async Task<IEnumerable<tblColleges>> GetCollages()
        {
            return await _context.tblColleges.Where(u => u.IsDeleted == false).ToListAsync();
        } 
        
        public async Task<IEnumerable<tblLevels>> GetLevels()
        {
            return await _context.tblLevels.Where(u => u.IsDeleted == false).ToListAsync();
        }



        public async Task<int> addCourse(CourseViewModel cou)
        {
            try
            {
                tblCourses course = new tblCourses();
                course.CourseNameAr = cou.CourseNameAr;
                course.CourseNameEn = cou.CourseNameEn;
                course.CourseNum = cou.CourseNum;
                course.CollegeId = cou.CollegeId;
                course.LevelId = cou.LevelId;
                course.GUID = Guid.NewGuid();

                _context.tblCourses.Add(course);
                int check = _context.SaveChanges();

                return check;
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        public async Task<bool> CourseNumEx(Guid id  , int CourseNum)
        {

            return await _context.tblCourses.Where(u => u.GUID != id).AnyAsync(u => u.CourseNum == CourseNum);
        }


        public async Task<int> UpdateCourse(CourseViewModel cou)
        {
            try
            {
                var sii = await _context.tblCourses.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == cou.GUID);
                tblCourses course = new tblCourses();

                course.Id = sii.Id;


                course.CourseNameAr = cou.CourseNameAr;
                course.CourseNameEn = cou.CourseNameEn;
                course.CourseNum = cou.CourseNum;
                course.CollegeId = cou.CollegeId;
                course.LevelId = cou.LevelId;
                course.GUID = cou.GUID;
                course.IsDeleted = cou.IsDeleted;


                _context.tblCourses.Update(course);
                int check = _context.SaveChanges();

                return check;
            }
            catch (Exception ex)
            {
                return 0;
            }
           
        }


        public async Task<CourseViewModel> GetCourseById(Guid id)
        {
            var user = await _context.tblCourses.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == id);

            CourseViewModel course = new CourseViewModel() ;

            course.CourseNameAr= user.CourseNameAr;
            course.CourseNameEn = user.CourseNameEn;
            course.CourseNum= user.CourseNum;
            course.CollegeId = user.CollegeId;
            course.LevelId= user.LevelId;

            course.GUID= user.GUID;

            return course;
        }

        public async Task<int> DeleteCourse(Guid id)
        {
            try
            {
                var course = await _context.tblCourses.FirstOrDefaultAsync(x => x.GUID == id);
                if (course != null)
                {
                    course.IsDeleted = true;
                    _context.tblCourses.Update(course);
                    int check = await _context.SaveChangesAsync();
                    return check;
                }
                else
                {
                    return 0; 
                }
            }
            catch (Exception ex) 
            {
                return 0;
            }
           
        }

    }
}
