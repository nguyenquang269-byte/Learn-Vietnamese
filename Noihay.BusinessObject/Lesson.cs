using System.ComponentModel.DataAnnotations;

namespace Noihay.BusinessObject;

public enum LessonType
{
    Vocabulary,
    Sentence,
    Story
}

public class Lesson
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public LessonType Type { get; set; } = LessonType.Vocabulary;

    public string? ImageUrl { get; set; }

    public ICollection<Word> Words { get; set; } = new List<Word>();
}
