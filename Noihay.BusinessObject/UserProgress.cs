using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Noihay.BusinessObject;

public class UserProgress
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = "Guest"; // Simple for now

    public int LessonId { get; set; }

    [ForeignKey("LessonId")]
    public Lesson? Lesson { get; set; }

    public int Stars { get; set; } // 0-3

    public int Score { get; set; }

    public DateTime CompletedAt { get; set; } = DateTime.Now;
}
