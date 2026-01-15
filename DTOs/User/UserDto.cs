using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Roles;
using go_han.Models;

namespace go_han.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public RoleReadDto Role { get; set; } = null!;
    }
}