using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Noihay.BusinessObject;

public class Word
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Text { get; set; } = string.Empty;

    public string? PronunciationGuide { get; set; }

    public string? ImageUrl { get; set; }

    public string? AudioUrl { get; set; }

    public int LessonId { get; set; }

    [ForeignKey("LessonId")]
    [JsonIgnore]
    public Lesson? Lesson { get; set; }
}
