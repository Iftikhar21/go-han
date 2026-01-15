using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs;
using go_han.DTOs.Roles;
using go_han.Models;

namespace go_han.Mappers.Roles
{
    public static class RoleMapper
    {
        public static RoleReadDto ToReadDto(Role role)
        {
            return new RoleReadDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }

        public static Role ToEntity(RoleCreateDto dto)
        {
            return new Role
            {
                RoleName = dto.RoleName
            };
        }

        public static void ApplyUpdate(Role role, RoleUpdateDto dto)
        {
            role.RoleName = dto.RoleName;
        }
    }
}