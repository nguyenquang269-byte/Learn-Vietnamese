using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Noihay.Services.Interfaces;
using Noihay.BusinessObject;

namespace Noihay.Web.Pages.Games;

public class KhiConLeoNuiModel : PageModel
{
    private readonly ILessonService _lessonService;

    public KhiConLeoNuiModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public List<SpellingQuestion> Questions { get; set; } = new();

    public class SpellingQuestion
    {
        public string IncompleteWord { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public string Hint { get; set; } = string.Empty;
    }

    public async Task OnGetAsync()
    {
        // For MVP, we'll hardcode some fun spelling challenges
        // In a real app, these would come from the database or an AI generator
        Questions = new List<SpellingQuestion>
        {
            new SpellingQuestion { 
                IncompleteWord = "Quả x...o", 
                CorrectAnswer = "oài", 
                Options = new List<string> { "oài", "oay", "uê" },
                Hint = "Một loại quả ngọt lịm màu vàng"
            },
            new SpellingQuestion { 
                IncompleteWord = "L... lấp", 
                CorrectAnswer = "Loáng", 
                Options = new List<string> { "Loáng", "Noáng", "Láng" },
                Hint = "Ánh sáng phản chiếu nhanh"
            },
            new SpellingQuestion { 
                IncompleteWord = "Ngọt ng...o", 
                CorrectAnswer = "ào", 
                Options = new List<string> { "ào", "ao", "au" },
                Hint = "Vị của mật ong"
            },
             new SpellingQuestion { 
                IncompleteWord = "Cái x...ng", 
                CorrectAnswer = "ẻng", 
                Options = new List<string> { "ẻng", "ẻn", "eng" },
                Hint = "Để bé xúc cát chơi"
            }
        };
    }
}
