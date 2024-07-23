using Bader.Models;
using Microsoft.EntityFrameworkCore;

namespace Bader.Domain
{
    public class UserDomain
    {
        private readonly BaaderContext _context;

        public UserDomain(BaaderContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tblUsers>>  GetUsers()
        {
            return await _context.tblUsers.ToListAsync();
        }
    }
}
