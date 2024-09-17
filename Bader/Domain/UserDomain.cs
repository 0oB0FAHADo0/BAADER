using Bader.Models;
using Bader.ViewModels;
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
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await _context.tblUsers.Select(x => new UserViewModel
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                FullNameAr = x.FullNameAr,
                FullNameEn = x.FullNameEn,
                Password = x.Password,
                Usertype = x.Usertype,
                Phone = x.Phone,
                CollegeNameAr = x.CollegeNameAr,
                CollegeNameEn = x.CollegeNameEn,
                CollegeCode = x.CollegeCode
            }).ToListAsync();
        }
        public async Task AddUser(UserViewModel user)
        {
            tblUsers userx = new tblUsers();
            userx.Id = user.Id;
            userx.Username = user.Username;
            userx.Email = user.Email;
            userx.FullNameAr = user.FullNameAr;
            userx.FullNameEn = user.FullNameEn;
            userx.Password = user.Password;
            userx.Usertype = user.Usertype;
            userx.Phone = user.Phone;
            userx.CollegeNameAr = user.CollegeNameAr;
            userx.CollegeNameEn = user.CollegeNameEn;
            userx.CollegeCode = user.CollegeCode;
            _context.tblUsers.Add(userx);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(UserViewModel user)
        {
            tblUsers userx = new tblUsers();
            userx.Id = user.Id;
            userx.Username = user.Username;
            userx.Email = user.Email;
            userx.FullNameAr = user.FullNameAr;
            userx.FullNameEn = user.FullNameEn;
            userx.Password = user.Password;
            userx.Usertype = user.Usertype;
            userx.Phone = user.Phone;
            userx.CollegeNameAr = user.CollegeNameAr;
            userx.CollegeNameEn = user.CollegeNameEn;
            userx.CollegeCode = user.CollegeCode;
            _context.tblUsers.Update(userx);
            await _context.SaveChangesAsync();
        }
        public async Task<UserViewModel> GetUserById(int id)
        {
            var user = await _context.tblUsers.FindAsync(id);
            UserViewModel userx = new UserViewModel();
            userx.Id = id;
            userx.Username = user.Username;
            userx.Email = user.Email;
            userx.FullNameAr = user.FullNameAr;
            userx.FullNameEn = user.FullNameEn;
            userx.Password = user.Password;
            userx.Usertype = user.Usertype;
            userx.Phone = user.Phone;
            userx.CollegeNameAr = user.CollegeNameAr;
            userx.CollegeNameEn = user.CollegeNameEn;
            userx.CollegeCode = user.CollegeCode;
            return userx;
        }
        public async Task<UserViewModel> ValidateUser(string username, string password)
        {
            var user = await _context.tblUsers.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
                return null;

            // Fetch the user's role information
            var userPermission = await _context.tblPermissions.FirstOrDefaultAsync(p => p.Username == username);
            tblRoles userRole = null;
            if (userPermission != null)
            {
                userRole = await _context.tblRoles.FirstOrDefaultAsync(r => r.Id == userPermission.RoleId);
            }

            return new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FullNameAr = user.FullNameAr,
                FullNameEn = user.FullNameEn,
                Usertype = user.Usertype,
                Phone = user.Phone,
                CollegeNameAr = user.CollegeNameAr,
                CollegeNameEn = user.CollegeNameEn,
                CollegeCode = user.CollegeCode,
                RoleNameEn = userRole?.RoleNameEn ?? "RegularUser"
            };
        }
        public async Task<bool> EmailExists(int id, string email)
        {
            return await _context.tblUsers.Where(u => u.Id != id).AnyAsync(u => u.Email == email);
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.tblUsers.FindAsync(id);
            if (user != null)
            {   //user.isdeleted == true do it to other tables with IsDeleted
                _context.tblUsers.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<UserViewModel> GetUsersByUsername(string username)
        {
            var user = await _context.tblUsers
                .Where(u => u.Username == username)
                .Select(x => new UserViewModel
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    FullNameAr = x.FullNameAr,
                    FullNameEn = x.FullNameEn,
                    Password = x.Password,
                    Usertype = x.Usertype,
                    Phone = x.Phone,
                    CollegeNameAr = x.CollegeNameAr,
                    CollegeNameEn = x.CollegeNameEn,
                    CollegeCode = x.CollegeCode,
                    Gender=x.Gender,
                })
                .FirstOrDefaultAsync(); 

            return user;
        }

    }
}
