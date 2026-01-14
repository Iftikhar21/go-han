using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Roles
{
    public class RoleUpdateDto
    {
        [Required, StringLength(50)]
        public string RoleName { get; set; } = string.Empty;
    }
}