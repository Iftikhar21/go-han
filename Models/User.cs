using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public required string Username { get; set; }
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string PasswordHash { get; set; }

        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigasi untuk relasi lain
        public virtual ICollection<ProjectMember> ProjectMemberships { get; set; } = null!;
    }
}