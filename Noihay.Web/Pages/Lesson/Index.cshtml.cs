using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Noihay.Services.Interfaces;
using Noihay.Services;
using Noihay.BusinessObject;

namespace Noihay.Web.Pages.Lesson;

public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public BusinessObject.Lesson? Lesson { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Lesson = await _lessonService.GetLessonByIdAsync(id);
        if (Lesson == null) return NotFound();

        // Ensure words are loaded (if not using Include in the service)
        Lesson.Words = (await _lessonService.GetWordsByLessonIdAsync(id)).ToList();

        return Page();
    }

    public async Task<JsonResult> OnGetEvaluate(string text)
    {
        var result = await _lessonService.EvaluatePronunciationAsync(text, "base64-audio-placeholder");
        return new JsonResult(result);
    }
}
