using Bader.Models;

namespace Bader.Domain
{
    public class permissionDomain
    {
        private readonly BaaderContext _context;

        public permissionDomain(BaaderContext context)
        {
            _context = context;
        }
        public IEnumerable<tblPermissions> GetTblPermissions()
        {
            return _context.tblPermissions.Where(x => x.IsDeleted == false);
        }
    }
}
