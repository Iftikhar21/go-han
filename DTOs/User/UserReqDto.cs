using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.User
{
    public class UserReqDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string PasswordHash { get ; set; } = null!;
        public required int RoleId { get; set; }
    }
}