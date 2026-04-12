using Noihay.Web.Models;
using Noihay.Web.Constants;

namespace Noihay.Web.Services;

public interface IGameService
{
    Task<GameLevelViewModel> GetLessonLevelAsync(int lessonId);
    Task<GameLevelViewModel> GetSpellingMonkeyLevelAsync();
}

public class GameService : IGameService
{
    private readonly ILessonService _lessonService;

    public GameService(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public async Task<GameLevelViewModel> GetLessonLevelAsync(int lessonId)
    {
        var lesson = await _lessonService.GetLessonByIdAsync(lessonId);
        if (lesson == null) return new GameLevelViewModel();

        var words = await _lessonService.GetWordsByLessonIdAsync(lessonId);
        
        return new GameLevelViewModel
        {
            Title = lesson.Title,
            Steps = words.Select(w => new GameStepViewModel
            {
                Text = w.Text,
                Type = GameConstants.GameTypes.Vocabulary,
                ImageUrl = string.IsNullOrEmpty(w.ImageUrl) || w.ImageUrl.Contains("placeholder") 
                    ? GameConstants.DefaultMascotImage : w.ImageUrl,
                AudioUrl = w.AudioUrl,
                PronunciationGuide = w.PronunciationGuide
            }).ToList()
        };
    }

    public async Task<GameLevelViewModel> GetSpellingMonkeyLevelAsync()
    {
        // For demonstration, returning a hardcoded level matching the previous implementation
        return new GameLevelViewModel
        {
            Title = "Khỉ con leo núi",
            Steps = new List<GameStepViewModel>
            {
                new() { 
                    Text = "Quả x...o", 
                    Type = GameConstants.GameTypes.Spelling, 
                    CorrectValue = "oài", 
                    Options = new() { "oài", "oay", "uê" }, 
                    Hint = "Quả ngọt màu vàng" 
                },
                new() { 
                    Text = "L... lấp", 
                    Type = GameConstants.GameTypes.Spelling, 
                    CorrectValue = "Loáng", 
                    Options = new() { "Loáng", "Noáng", "Láng" }, 
                    Hint = "Ánh sáng phản chiếu" 
                }
            }
        };
    }
}
