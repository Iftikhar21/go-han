using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Repositories.IRepository;
using go_han.Models;
using Microsoft.EntityFrameworkCore;

namespace go_han.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles
                .OrderBy(r => r.Id)
                .ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task<Role?> CreateRoleAsync(Role role)
        {
            var exist = await GetRoleByNameAsync(role.RoleName);
            if (exist is not null)
                return null;

            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> UpdateRoleAsync(int id, Role role)
        {
            var exist = await _context.Roles.FindAsync(id);
            if (exist is null)
                return null;

            var sameName = await GetRoleByNameAsync(role.RoleName);
            if (sameName is not null && sameName.Id != id)
                return null;

            exist.RoleName = role.RoleName;

            await _context.SaveChangesAsync();
            return exist;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var exist = await _context.Roles.FindAsync(id);
            if (exist is null)
                return false;

            _context.Roles.Remove(exist);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}