using Noihay.BusinessObject;
using Noihay.DataAccessLayer.Interfaces;
using Noihay.Services.Interfaces;

namespace Noihay.Services;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _unitOfWork;

    public LessonService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsAsync() => 
        await _unitOfWork.Repository<Lesson>().GetAllAsync();

    public async Task<Lesson?> GetLessonByIdAsync(int id) => 
        await _unitOfWork.Repository<Lesson>().GetByIdAsync(id);

    public async Task<IEnumerable<Word>> GetWordsByLessonIdAsync(int lessonId) => 
        await _unitOfWork.Repository<Word>().FindAsync(w => w.LessonId == lessonId);

    public async Task SaveProgressAsync(UserProgress progress)
    {
        await _unitOfWork.Repository<UserProgress>().AddAsync(progress);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserProgress>> GetUserProgressAsync(string userId) => 
        await _unitOfWork.Repository<UserProgress>().FindAsync(p => p.UserId == userId);

    public async Task<(bool IsCorrect, int Score, string Feedback)> EvaluatePronunciationAsync(string expectedText, string audioData)
    {
        // Placeholder for AI/STT logic
        // In a real app, you'd send 'audioData' to a speech recognition service
        await Task.Delay(500); // Simulate processing delay

        var random = new Random();
        var score = random.Next(60, 100);
        var isCorrect = score > 75;

        string feedback = isCorrect ? "Giỏi quá! 🌟" : "Thử lại nhé! 💪";
        if (score > 90) feedback = "Xuất sắc! 🔥";

        return (isCorrect, score, feedback);
    }
}
