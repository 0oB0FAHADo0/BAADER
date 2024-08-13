using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Bader.Domain
{
    public class LevelDomain
    {
        private readonly BaaderContext _context;

        public LevelDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LevelViewModel>> GetLevels()
        {

            return await _context.tblLevels.Where(u => u.IsDeleted == false).Select(x => new LevelViewModel
            {

                LevelNameAr = x.LevelNameAr,
                LevelNameEn = x.LevelNameEn,
                LevelNum = x.LevelNum,
                GUID = x.GUID,

            }).ToListAsync();

        }

        public async Task<int> AddLevel(LevelViewModel Level)
        {
            try
            {
                tblLevels Levelx = new tblLevels();


                Levelx.LevelNameAr = Level.LevelNameAr;
                Levelx.LevelNameEn = Level.LevelNameEn;
                Levelx.LevelNum = Level.LevelNum;
                Levelx.GUID = Guid.NewGuid();
                Levelx.IsDeleted = false;

                _context.tblLevels.Add(Levelx);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }


        }

        public async Task<LevelViewModel> GetLevelbyId(Guid gudi)
        {
            var Level = _context.tblLevels.AsNoTracking().FirstOrDefault(tblLevels => tblLevels.GUID == gudi);


            LevelViewModel Levelx = new LevelViewModel();
            Levelx.LevelNameAr = Level.LevelNameAr;
            Levelx.LevelNameEn = Level.LevelNameEn;
            Levelx.LevelNum = Level.LevelNum;
            Levelx.GUID = Level.GUID;

            return Levelx;
        }
        public int GetLevelIdByGuid(Guid gudi)
        {

            var Level = _context.tblLevels.AsNoTracking().FirstOrDefault(tblLevels => tblLevels.GUID == gudi);

            return Level.Id;

        }

        public async Task<int> UpdateLevel(LevelViewModel Level)
        {

            try
            {
                tblLevels Levelx = new tblLevels();


                Levelx.Id = GetLevelIdByGuid(Level.GUID);

                Levelx.GUID = Level.GUID;


                Levelx.LevelNameAr = Level.LevelNameAr;
                Levelx.LevelNameEn = Level.LevelNameEn;
                Levelx.LevelNum = Level.LevelNum;
                Levelx.IsDeleted = Level.IsDeleted;

                _context.tblLevels.Update(Levelx);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }
        public async Task<bool> LevelNumExists(int LevelNum, Guid guid)
        {

            //var CheckExist = _context.tblLevels.SingleOrDefault(n => n.BuildingNum == BuildingNum);
            return await _context.tblLevels.Where(u => u.GUID != guid).Where(u => u.IsDeleted == false).AnyAsync(u => u.LevelNum == LevelNum);

        }

    }
}
