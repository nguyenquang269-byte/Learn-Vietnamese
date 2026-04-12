using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Noihay.Services.Interfaces;
using Noihay.BusinessObject;

namespace Noihay.Web.Pages.Games;

public class PhuThuySuaAmModel : PageModel
{
    private readonly ILessonService _lessonService;

    public PhuThuySuaAmModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public List<Word> TargetWords { get; set; } = new();

    public async Task OnGetAsync()
    {
        // Load some practice words (specifically for L/N, S/X etc)
        var allWords = await _lessonService.GetWordsByLessonIdAsync(1); // Default to first lesson for now
        TargetWords = allWords.ToList();
    }

    public async Task<JsonResult> OnGetEvaluate(string text)
    {
        var result = await _lessonService.EvaluatePronunciationAsync(text, "game-audio");
        return new JsonResult(result);
    }
}
