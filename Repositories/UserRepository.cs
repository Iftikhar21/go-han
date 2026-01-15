using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Models;
using go_han.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace go_han.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.Include(x => x.Role).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return null;

            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return null;

            return user;
        }

        public async Task<User> CreatedUserAsync(User user)
        {
            var existUser = await GetUserByEmailAsync(user.Email);
            if (existUser != null)
                return null!;

            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();


            return user;
        }

        public async Task<User?> UpdateUserAsync(int id, User user)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null)
                return null;

            var emailOwner = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Id != id);
            if (emailOwner != null)
                return null;

            existUser.Username = user.Username;
            existUser.Email = user.Email;
            existUser.RoleId = user.RoleId;

            await _context.SaveChangesAsync();

            return existUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null)
                return false;

            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateRoleUserAsync(int id, int roleId)
        {
            var existUser = await _context.Users.FindAsync(id);
            if (existUser == null)
                return false;

            existUser.RoleId = roleId;
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}