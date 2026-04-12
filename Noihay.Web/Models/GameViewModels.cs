namespace Noihay.Web.Models;

public class GameLevelViewModel
{
    public string Title { get; set; } = string.Empty;
    public List<GameStepViewModel> Steps { get; set; } = new();
}

public class GameStepViewModel
{
    public string Text { get; set; } = string.Empty;
    public string Type { get; set; } = "vocabulary"; // vocabulary, spelling, pronunciation
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? PronunciationGuide { get; set; }
    public string? Hint { get; set; }
    public List<string>? Options { get; set; }
    public string? CorrectValue { get; set; }
}
