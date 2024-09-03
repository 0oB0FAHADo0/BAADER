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
            try
            {
                return await _context.tblCourses.Include(x => x.College).Include(x => x.Level).Include(x => x.Major).Where(x => x.IsDeleted == false).Select(x => new CourseViewModel
                {
                    CourseNum = x.CourseNum,
                    CourseNameAr = x.CourseNameAr,
                    CourseNameEn = x.CourseNameEn,
                    CollegeId = x.CollegeId,
                    LevelId = x.LevelId,
                    CollageNameAr = x.College.CollegeNameAr,
                    LevelNameAr = x.Level.LevelNameAr,
                    MajorId = x.MajorId,
                    MajorNameAr = x.Major.MajorNameAr,
                    GUID = x.GUID,

                }).ToListAsync();
            }
            catch (Exception ex) {
            
                return new List<CourseViewModel>();

            }
            
        }

        public async Task<IEnumerable<CourseViewModel>> GetSomeCourses(string collagecode)
        {
           // int UserId = 1;



            return await _context.tblCourses.Include(x => x.Level).Include(x => x.College).Where(x => x.IsDeleted == false).Where(x => x.College.CollegeCode == collagecode ).Select(x => new CourseViewModel
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
            try
            {
                return await _context.tblColleges.Where(u => u.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex) {
                return new List<tblColleges>();
            }
            
        } 
        
        public async Task<IEnumerable<tblLevels>> GetLevels()
        {
            try
            {
                return await _context.tblLevels.Where(u => u.IsDeleted == false).ToListAsync();
            }
            catch
            {
                return new List<tblLevels>();
            }
           
        }

        public async Task<IEnumerable<tblMajors>> GetMajors()
        {
            try
            {
                return await _context.tblMajors.Where(u => u.IsDeleted == false).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<tblMajors>();
            }

        }



        public async Task<int> addCourse(CourseViewModel cou,string username , String CollageCode)
        {
            try
            {
                var college = await _context.tblColleges.Where(c => c.CollegeCode == CollageCode && c.IsDeleted == false).FirstOrDefaultAsync();

                if (college == null)
                {
                    // Handle the case where the college is not found
                    return 0;
                }

                tblCourses course = new tblCourses();
                course.CourseNameAr = cou.CourseNameAr;
                course.CourseNameEn = cou.CourseNameEn;
                course.CourseNum = cou.CourseNum;
                course.CollegeId = college.Id;
                course.LevelId = cou.LevelId;
                course.MajorId = cou.MajorId;
                course.GUID = Guid.NewGuid();
                

                _context.tblCourses.Add(course);

                _context.SaveChanges();
                
                    tblCoursesLogs log = new tblCoursesLogs();
                    log.CourseId = course.Id;
                    log.OperationType = "Add";
                    log.DateTime = DateTime.Now;
                    log.CreatedBy = username;
                    log.AdditionalInfo = $"تم إضافة مقرر {course.CourseNameAr} بواسطة هذا المستخدم";
                    _context.tblCoursesLogs.Add(log);
                

                int check =await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {
                
                return 0;
            }
            
        }

        public async Task<bool> CourseNumEx(Guid id  , string CourseNum)
        {

            return await _context.tblCourses.Where(u => u.GUID != id).AnyAsync(u => u.CourseNum == CourseNum);
        }


        public async Task<int> UpdateCourse(CourseViewModel cou , String username)
        {
            try
            {
                var sii = await _context.tblCourses.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == cou.GUID);
                tblCourses course = new tblCourses();

                course.Id = sii.Id;


                course.CourseNameAr = cou.CourseNameAr;
                course.CourseNameEn = cou.CourseNameEn;
                course.CourseNum = cou.CourseNum;
                course.CollegeId = cou.CollegeId ;
                course.LevelId = cou.LevelId;
                course.MajorId = cou.MajorId;
                course.GUID = cou.GUID;
                course.IsDeleted = cou.IsDeleted;


                _context.tblCourses.Update(course);

                _context.SaveChanges();
                
                    tblCoursesLogs log = new tblCoursesLogs();
                    log.CourseId = course.Id;
                    log.OperationType = "Update";
                    log.DateTime = DateTime.Now;
                    log.CreatedBy = username;
                    log.AdditionalInfo = $"تم تحديث مقرر {course.CourseNameAr} بواسطة هذا المستخدم";
                    _context.tblCoursesLogs.Add(log);
                

                int check =await _context.SaveChangesAsync();

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

        public async Task<int> DeleteCourse(Guid id , String username)
        {
            try
            {
                var course = await _context.tblCourses.FirstOrDefaultAsync(x => x.GUID == id);
                if (course != null)
                {
                    course.IsDeleted = true;
                    _context.tblCourses.Update(course);

                    _context.SaveChanges() ;
                    
                        tblCoursesLogs log = new tblCoursesLogs();
                        log.CourseId = course.Id;
                        log.OperationType = "Delete";
                        log.DateTime = DateTime.Now;
                        log.CreatedBy = username;
                        log.AdditionalInfo = $"تم حذف مقرر {course.CourseNameAr} بواسطة هذا المستخدم";
                        _context.tblCoursesLogs.Add(log);
                    

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

        public async Task<tblColleges> GetCollageById(int id)
        {
            try
            {
                return await _context.tblColleges.FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
            }
            catch
            {
                return null;
            }
        }


    }
}
