﻿//using Bader.Migrations;
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

        public async Task<IEnumerable<MajorViewModel>> GetSomeMajors(string collagecode)
        {
            try
            {
                  return await _context.tblMajors.Include(x => x.College).Where(x => x.IsDeleted == false && x.College.CollegeCode == collagecode).Select(x => new MajorViewModel { 
                  MajorNameAr = x.MajorNameAr,
                  MajorNameEn = x.MajorNameEn,
                  CollageId = x.CollegeId,
                  CollageNameAr = x.College.CollegeNameAr,
                  GUID=x.GUID,
                  }).ToListAsync() ;
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
        public async Task<int> addMajors(MajorViewModel Major, string username)
        {
            try
            {
                //var college = await _context.tblColleges.Where(c =>c.IsDeleted == false).FirstOrDefaultAsync();

                //if (college == null)
                //{

                //    return 0;
                //}

                tblMajors Majorx = new tblMajors();
                Majorx.MajorNameAr = Major.MajorNameAr;
                Majorx.MajorNameEn = Major.MajorNameEn;
                Majorx.CollegeId = Major.CollageId;
                Majorx.GUID = Guid.NewGuid();


                _context.tblMajors.Add(Majorx);

                _context.SaveChanges();

                tblMajorsLogs log = new tblMajorsLogs();
                //log.Id = Majorx.Id;
                log.OperationType = "Add";
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.AdditionalInfo = $"تم إضافة مقرر {Majorx.MajorNameAr} بواسطة هذا المستخدم";
                _context.tblMajorsLogs.Add(log);


                int check = await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }

        public async Task<int> addMajor(MajorViewModel Major, string username , string collagecode)
        {
            try
            {
                var college = await _context.tblColleges.Where(c => c.IsDeleted == false && c.CollegeCode == collagecode).FirstOrDefaultAsync();

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

                tblMajorsLogs log = new tblMajorsLogs();
                //log.Id = Majorx.Id;
                log.OperationType = "Add";
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.AdditionalInfo = $"تم إضافة مقرر {Majorx.MajorNameAr} بواسطة هذا المستخدم";
                _context.tblMajorsLogs.Add(log);


                int check = await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {

                return 0;
            }

        }
        public async Task<int> UpdateMajors(MajorViewModel major, String username)
        {
            try
            {

                var maj = await _context.tblMajors.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == major.GUID);
                tblMajors majorx = new tblMajors();

                majorx.Id = maj.Id;


                majorx.MajorNameAr = major.MajorNameAr;
                majorx.MajorNameEn = major.MajorNameEn;
                majorx.CollegeId = major.CollageId;
                majorx.GUID = major.GUID;
                majorx.IsDeleted = major.IsDeleted;


                _context.tblMajors.Update(majorx);

                _context.SaveChanges();

                tblMajorsLogs log = new tblMajorsLogs();
                log.OperationType = "Update";
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.AdditionalInfo = $"تم إضافة مقرر {majorx.MajorNameAr} بواسطة هذا المستخدم";
                _context.tblMajorsLogs.Add(log);


                int check = await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public async Task<int> UpdateMajor(MajorViewModel major, String username, string collagecode)
        {
            try
            {
                var collage = await _context.tblColleges.Where( x => x.IsDeleted == false && x.CollegeCode == collagecode ).FirstOrDefaultAsync(); 

                var maj = await _context.tblMajors.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == major.GUID);
                tblMajors majorx = new tblMajors();

                majorx.Id = maj.Id;


                majorx.MajorNameAr = major.MajorNameAr;
                majorx.MajorNameEn = major.MajorNameEn;
                majorx.CollegeId = collage.Id;
                majorx.GUID = major.GUID;
                majorx.IsDeleted = major.IsDeleted;


                _context.tblMajors.Update(majorx);

                _context.SaveChanges();

                tblMajorsLogs log = new tblMajorsLogs();
                log.OperationType = "Update";
                log.DateTime = DateTime.Now;
                log.CreatedBy = username;
                log.AdditionalInfo = $"تم إضافة مقرر {majorx.MajorNameAr} بواسطة هذا المستخدم";
                _context.tblMajorsLogs.Add(log);


                int check = await _context.SaveChangesAsync();

                return check;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public async Task<MajorViewModel> GetMajorsById(Guid id)
        {
            var maj = await _context.tblMajors.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == id);

            MajorViewModel majorx = new MajorViewModel();

            majorx.MajorNameAr = maj.MajorNameAr;
            majorx.MajorNameEn = maj.MajorNameEn;
            majorx.CollageId = maj.CollegeId;


            majorx.GUID = maj.GUID;

            return majorx;
        }
        public async Task<int> DeleteMajors(Guid id, String username)
        {
            try
            {
                var major = await _context.tblMajors.FirstOrDefaultAsync(x => x.GUID == id);
                if (major != null)
                {
                    major.IsDeleted = true;
                    _context.tblMajors.Update(major);

                    _context.SaveChanges();

                    tblMajorsLogs log = new tblMajorsLogs();
                    log.OperationType = "Delte";
                    log.DateTime = DateTime.Now;
                    log.CreatedBy = username;
                    log.AdditionalInfo = $"تم إضافة مقرر {major.MajorNameAr} بواسطة هذا المستخدم";
                    _context.tblMajorsLogs.Add(log);


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
