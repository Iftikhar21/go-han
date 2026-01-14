using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetRolesAsync();
        public Task<Role?> GetRoleByIdAsync(int id);
        public Task<Role?> GetRoleByNameAsync(string roleName);
        public Task<Role?> CreateRoleAsync(Role role);
        public Task<Role?> UpdateRoleAsync(int id, Role role);
        public Task<bool> DeleteRoleAsync(int id);
    }
}