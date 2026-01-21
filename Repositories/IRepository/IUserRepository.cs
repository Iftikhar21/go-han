using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsersAsync();
        public Task<User?> GetUserByIdAsync(int id);
        public Task<User?> GetUserByEmailAsync(string email);
        public Task<User> CreatedUserAsync(User user);
        public Task<User?> UpdateUserAsync(int id, User user);
        public Task<bool> DeleteUserAsync(int id);

        public Task<List<User?>> GetUsersByRoleAsync(int roleId);
        public Task<bool> UpdateRoleUserAsync(int id, int roleId);
    }
}