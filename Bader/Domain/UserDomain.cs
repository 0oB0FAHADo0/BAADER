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
        public async Task<IEnumerable<UserViewModel>>  GetUsers()
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
            _context.tblUsers.Update(userx);
            await _context.SaveChangesAsync();
        }
        public async Task<UserViewModel> GetUserById(int id)
        {
            var user= await _context.tblUsers.FindAsync(id);
            UserViewModel userx = new UserViewModel();
            userx.Id = id;
            userx.Username = user.Username;
            userx.Email = user.Email;
            userx.FullNameAr = user.FullNameAr;
            userx.FullNameEn = user.FullNameEn;
            userx.Password = user.Password;
            userx.Usertype = user.Usertype;
            userx.Phone = user.Phone;
            return userx;
        }
        public async Task<bool> EmailExists(int id,string email)
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
    }
}
