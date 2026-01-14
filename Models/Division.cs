using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.Models
{
    public class Division
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public required string DivisionName { get; set; }
        public virtual ICollection<ProjectMember> ProjectMembers { get; set; } = null!;
    }
}