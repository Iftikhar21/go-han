using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Projects
{
    public class ProjectMemberDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Division { get; set; } = null!;
    }
}