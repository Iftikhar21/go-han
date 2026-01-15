using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Division;
using go_han.DTOs.User;

namespace go_han.DTOs.Projects
{
    public class ProjectMemberResponseDto
    {
        public int Id { get; set; }
        public ProjectDetailDto Project { get; set; } = null!;
        public UserDto User { get; set; } = null!;
        public DivisionDTO Division { get; set; } = null!;
    }
}