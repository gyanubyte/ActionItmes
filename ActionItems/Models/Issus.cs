using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ActionItems.Models
{
    public class Issus
    {
        [Key]
        public int TaskId { get; set; }

        public string requester { get; set; }

        [Required]
        [StringLength(255)]
        public string Subject { get; set; }


        [Required]
        [EnumDataType(typeof(TaskStatus))]
        public TaskStatus Status { get; set; } = TaskStatus.Pending;


        [Required]
        [EnumDataType(typeof(TaskPriority))]
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public string Description { get; set; }

        //[ForeignKey("CreatedByUser")]
        public int? CreatedBy { get; set; }

        //[ForeignKey("AssignedToUser")]
        public int? AssignedTo { get; set; }

        public DateTime? DueDate { get; set; } = DateTime.Now;

        // Navigation properties for relationships
       // public virtual User CreatedByUser { get; set; }
       // public virtual User AssignedToUser { get; set; }
    }
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }


}
