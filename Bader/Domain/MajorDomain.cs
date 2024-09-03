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

    }
}
