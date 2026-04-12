using Noihay.BusinessObject;

namespace Noihay.Services.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<Lesson>> GetLessonsAsync();
    Task<Lesson?> GetLessonByIdAsync(int id);
    Task<IEnumerable<Word>> GetWordsByLessonIdAsync(int lessonId);
    Task SaveProgressAsync(UserProgress progress);
    Task<IEnumerable<UserProgress>> GetUserProgressAsync(string userId);
    Task<(bool IsCorrect, int Score, string Feedback)> EvaluatePronunciationAsync(string expectedText, string audioData);
}
