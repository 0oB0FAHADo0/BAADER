
using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
namespace Bader.Domain
{
    public class CollegeDomain
    {
        

        private readonly BaaderContext _context;

        public CollegeDomain(BaaderContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<CollegeViewModel>> GetAllColleges()
        {

            try
            {

                return await _context.tblColleges.Where(u => u.IsDeleted == false).Select(x => new CollegeViewModel
                {

                    CollegeCode = x.CollegeCode,
                    CollegeNameAr = x.CollegeNameAr,
                    CollegeNameEn = x.CollegeNameEn,
                    GUID = x.GUID,

                }).ToListAsync();


            }
            catch
            {
                return new List<CollegeViewModel>();

            }


        }

        public async Task<int> AddCollege(CollegeViewModel college) 
        {
            try
            {
                tblColleges collegex = new tblColleges();


                collegex.CollegeNameAr = college.CollegeNameAr;
                collegex.CollegeNameEn = college.CollegeNameEn;
                collegex.CollegeCode = college.CollegeCode;

                collegex.GUID = Guid.NewGuid();
                collegex.IsDeleted = false;

                _context.tblColleges.Add(collegex);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

            // CheckExist = _context.tblColleges.SingleOrDefault(n => n.BuildingNum == tblColleges.BuildingNum);
            
            
                

        }
        
        public async Task<CollegeViewModel> GetCollegebyId(Guid gudi)
        {
            try
            {
                var college = _context.tblColleges.AsNoTracking().FirstOrDefault(tblColleges => tblColleges.GUID == gudi);


                CollegeViewModel collegex = new CollegeViewModel();

                collegex.CollegeNameAr = college.CollegeNameAr;
                collegex.CollegeNameEn = college.CollegeNameEn;//
                collegex.CollegeCode = college.CollegeCode;
                collegex.GUID = college.GUID;

                return collegex;
            }
            catch
            {
                return null;
            }
        }
        public int GetCollegeIdByGuid(Guid gudi) {
            
                var college = _context.tblColleges.AsNoTracking().FirstOrDefault(tblColleges => tblColleges.GUID == gudi);

                return college.Id;
            
        
            
        }

        public async Task<int> UpdateCollege(CollegeViewModel college)
        {

            try
            {
                tblColleges collegex = new tblColleges();


                collegex.Id = GetCollegeIdByGuid(college.GUID);

                collegex.GUID = college.GUID;


                collegex.CollegeNameAr = college.CollegeNameAr;
                collegex.CollegeNameEn = college.CollegeNameEn;
                collegex.CollegeCode = college.CollegeCode;
                collegex.IsDeleted = college.IsDeleted;

                _context.tblColleges.Update(collegex);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }
           
        }
      
        public async Task<bool> CollegeCodeExists(string CollegeCode, Guid guid)
        {

            return await _context.tblColleges.Where(u => u.GUID != guid).Where(u => u.IsDeleted == false).AnyAsync(u => u.CollegeCode == CollegeCode);

        }


    }
}
