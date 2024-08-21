using Bader.Models;
using Bader.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Bader.Domain
{
    public class PermissionDomain
    {
        private readonly BaaderContext _context;

        public PermissionDomain(BaaderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionViewModel>> GetPermissions()
        {
            return await _context.tblPermissions.Where(x => x.IsDeleted == false).Select(x => new PermissionViewModel
            {
                //Id = x.Id,

                Username = x.Username,

                RoleId = x.RoleId,


                CollegeId = x.CollegeId,

                GUID = x.GUID,

                IsDeleted = x.IsDeleted,

            }).ToListAsync();
        }

        public async Task<IEnumerable<tblColleges>> GetColleges()
        {
            return await _context.tblColleges.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<tblRoles>> GetRoles()
        {
            return await _context.tblRoles.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task AddPermission(PermissionViewModel permission)
        {
            tblPermissions permissionx = new tblPermissions();
            //permissionx.Id = permission.Id;
            permissionx.Username = permission.Username;
            permissionx.RoleId = permission.RoleId;
            permissionx.CollegeId = permission.CollegeId;
            permissionx.GUID = Guid.NewGuid();
            permissionx.IsDeleted = permission.IsDeleted;
            _context.tblPermissions.Add(permissionx);
            await _context.SaveChangesAsync();
        }

        //public async Task<PermissionViewModel> GetPermissionByGUID(Guid id)
        //{
        //    var permission = await _context.tblPermissions.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == id);
        //    PermissionViewModel permission1 = new PermissionViewModel();
        //    permission1.Id = permission.Id;
        //    permission1.Username = permission.Username;
        //    permission1.RoleId = permission.RoleId;
        //    permission1.CollegeId = permission.CollegeId;
        //    permission1.GUID = permission.GUID;
        //    permission1.IsDeleted = permission.IsDeleted;
        //    return permission1;
        //}

        public async Task<PermissionViewModel> GetPermissionbyId(Guid id)
        {
            var permission = _context.tblPermissions.AsNoTracking().FirstOrDefault(tblPermissions => tblPermissions.GUID == id);


            PermissionViewModel permissions = new PermissionViewModel();

            //permissions.Id = permission.Id;
            permissions.Username = permission.Username;
            permissions.RoleId = permission.RoleId;
            permissions.CollegeId = permission.CollegeId;
            permissions.GUID = permission.GUID;
            permissions.IsDeleted = permission.IsDeleted;

            return permissions;
        }

            public tblPermissions GetPermissionIdByGUID(Guid id)
        {

            var permission = _context.tblPermissions.AsNoTracking().FirstOrDefault(tblPermissions => tblPermissions.GUID == id);

            return permission;



        }

        public async Task<int> UpdatePermission(PermissionViewModel permission)
        {

            try
            {

                tblPermissions permissions = GetPermissionIdByGUID(permission.GUID);
                //permissions.Id = permission.Id;
                permissions.Username = permission.Username;
                permissions.RoleId = permission.RoleId;
                permissions.CollegeId = permission.CollegeId;
                _context.tblPermissions.Update(permissions);
                int check = await _context.SaveChangesAsync();
                return check;
            }
            catch
            {
                return 0;
            }

        }


        //public async Task DeletePermission(PermissionViewModel permission)
        //{
        //    var permissionx = await _context.tblPermissions.AsNoTracking().FirstOrDefaultAsync(x => x.GUID == permission.GUID);
        //    tblPermissions Contents = new tblPermissions();
        //    permissionx.Id = permission.Id;
        //    permissionx.Username = permission.Username;
        //    permissionx.RoleId = permission.RoleId;
        //    permissionx.CollegeId = permission.CollegeId;
        //    permissionx.GUID = permission.GUID;
        //    permissionx.IsDeleted = permission.IsDeleted;
        //    permissionx.IsDeleted = true;
        //    _context.tblPermissions.Update(permissionx);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<bool> PermissionIdExists(Guid guid)
        {

            return await _context.tblPermissions.Where(u => u.IsDeleted == false).AnyAsync(u => u.GUID == guid);

        }

        public async Task<int> DeletePermission(Guid id)
        {
            try
            {
                var permission = _context.tblPermissions.FirstOrDefault(u => u.GUID == id);
                if (permission != null)
                {
                    permission.IsDeleted = true;
                    _context.Update(permission);
                    _context.SaveChanges();

                    return 1;
                }
                else
                    return 0;
            }
            catch ( Exception ex)
            {
                return 0;
            }

        }
        //public bool IsRoleNameDuplicate(string name, Guid? Permissionn = null)
        //{
        //    if (Permissionn == null)
        //    {
        //        // Check if there is any non-deleted permission with the given user name
        //        return _context.tblPermissions.Any(u => u.Username == name && !u.IsDeleted);
        //    }
        //    else
        //    {
        //        // Check if there is any non-deleted permission with the given user name
        //        // excluding the permission with the specified Permissionn ID
        //        return _context.tblPermissions.Any(u => u.Username == name && u.Id != Permissionn && !u.IsDeleted);
        //    }
        //}
        public int GetPermissionIdByGuid(Guid gudi)
        {

            var permissions = _context.tblPermissions.AsNoTracking().FirstOrDefault(tblPermissions => tblPermissions.GUID == gudi);

            return permissions.Id;



        }
    }
}

