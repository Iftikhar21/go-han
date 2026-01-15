using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Roles;
using go_han.DTOs.User;
using go_han.Models;

namespace go_han.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,

                Role = new RoleReadDto
                {
                    Id = user.Role.Id,
                    RoleName = user.Role.RoleName
                }
            };
        }

        public static User ToUserReqDto(this UserReqDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                RoleId = userDto.RoleId
            };
        }
    }
}