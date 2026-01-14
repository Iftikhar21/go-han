using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.TaskDTOs
{
    public class TaskRequestDTO
    {
    [Required]
    public int ProjectId { get; set; }

    [Required]
    public int AssigneeId { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    [Required]
    public string Difficulty { get; set; } = string.Empty;

    public DateTime? Deadline { get; set; }
    }
}