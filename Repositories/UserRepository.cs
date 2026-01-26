using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Interface;
using go_han.Models;
using go_han.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace go_han.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordUtils _hasher;

        public UserRepository(AppDbContext context, IPasswordUtils hasher)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
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

            user.PasswordHash = _hasher.HashPassword(user.PasswordHash);
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
            if(string.IsNullOrEmpty(user.PasswordHash))
                existUser.PasswordHash = existUser.PasswordHash;
            else
                existUser.PasswordHash = _hasher.HashPassword(user.PasswordHash);
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

        public async Task<List<User?>> GetUsersByRoleAsync(int roleId)
        {
            var users = await _context.Users.Include(x => x.Role).Where(x => x.RoleId == roleId).ToListAsync();
            if (!users.Any())
                return null!;

            return users!;
        }

        public async Task<List<User?>> GetUsersEmployeeAsync()
        {
            var users = await _context.Users.Include(x => x.Role).Where(x => x.RoleId == 2).ToListAsync();
            if (!users.Any())
                return null!;

            return users!;
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