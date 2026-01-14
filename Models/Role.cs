using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace go_han.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public required string RoleName { get; set; }
        public virtual ICollection<User> Users { get; set; } = null!;
    }
}