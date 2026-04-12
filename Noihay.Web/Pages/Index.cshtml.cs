namespace Noihay.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILessonService _lessonService;

    public IndexModel(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    public List<Lesson> Lessons { get; set; } = new();

    public async Task OnGetAsync()
    {
        Lessons = await _lessonService.GetAllLessonsAsync();
    }
}
