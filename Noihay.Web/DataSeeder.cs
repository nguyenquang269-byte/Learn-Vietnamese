using Noihay.DataAccessLayer;
using Noihay.Services;
using Noihay.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace Noihay.Web;

public static class DataSeeder
{
    public static async Task SeedAsync(NoihayDbContext context)
    {
        if (await context.Lessons.AnyAsync()) return;

        var lessons = new List<Lesson>
        {
            new Lesson 
            { 
                Title = "Động vật quanh em", 
                Description = "Học tên các con vật quen thuộc",
                Type = LessonType.Vocabulary,
                ImageUrl = "/images/categories/animals.png",
                Words = new List<Word>
                {
                    new Word { Text = "Con cá", PronunciationGuide = "Cơn cá", ImageUrl = "/images/words/fish.png", AudioUrl = "/audio/fish.mp3" },
                    new Word { Text = "Con chó", PronunciationGuide = "Cơn chó", ImageUrl = "/images/words/dog.png", AudioUrl = "/audio/dog.mp3" },
                    new Word { Text = "Con mèo", PronunciationGuide = "Cơn mèo", ImageUrl = "/images/words/cat.png", AudioUrl = "/audio/cat.mp3" },
                    new Word { Text = "Con lợn", PronunciationGuide = "Cơn lợn", ImageUrl = "/images/words/pig.png", AudioUrl = "/audio/pig.mp3" },
                    new Word { Text = "Con gà", PronunciationGuide = "Cơn gà", ImageUrl = "/images/words/chicken.png", AudioUrl = "/audio/chicken.mp3" }
                }
            },
            new Lesson 
            { 
                Title = "Hoa quả tươi ngon", 
                Description = "Khám phá thế giới trái cây",
                Type = LessonType.Vocabulary,
                ImageUrl = "/images/categories/fruits.png",
                Words = new List<Word>
                {
                    new Word { Text = "Quả táo", PronunciationGuide = "Quả táo", ImageUrl = "/images/words/apple.png", AudioUrl = "/audio/apple.mp3" },
                    new Word { Text = "Quả chuối", PronunciationGuide = "Quả chuối", ImageUrl = "/images/words/banana.png", AudioUrl = "/audio/banana.mp3" },
                    new Word { Text = "Quả cam", PronunciationGuide = "Quả cam", ImageUrl = "/images/words/orange.png", AudioUrl = "/audio/orange.mp3" },
                    new Word { Text = "Quả dứa", PronunciationGuide = "Quả dứa", ImageUrl = "/images/words/pineapple.png", AudioUrl = "/audio/pineapple.mp3" },
                    new Word { Text = "Quả dâu", PronunciationGuide = "Quả dâu", ImageUrl = "/images/words/strawberry.png", AudioUrl = "/audio/strawberry.mp3" }
                }
            },
            new Lesson 
            { 
                Title = "Đồ dùng học tập", 
                Description = "Chuẩn bị đi học cùng bé",
                Type = LessonType.Vocabulary,
                ImageUrl = "/images/categories/school.png",
                Words = new List<Word>
                {
                    new Word { Text = "Cái bút", PronunciationGuide = "Cái bút", ImageUrl = "/images/words/pen.png", AudioUrl = "/audio/pen.mp3" },
                    new Word { Text = "Quyển vở", PronunciationGuide = "Quyển vở", ImageUrl = "/images/words/notebook.png", AudioUrl = "/audio/notebook.mp3" },
                    new Word { Text = "Cái cặp", PronunciationGuide = "Cái cặp", ImageUrl = "/images/words/bag.png", AudioUrl = "/audio/bag.mp3" },
                    new Word { Text = "Cục tẩy", PronunciationGuide = "Cục tẩy", ImageUrl = "/images/words/eraser.png", AudioUrl = "/audio/eraser.mp3" }
                }
            },
            new Lesson 
            { 
                Title = "Gia đình yêu thương", 
                Description = "Bé gọi tên những người thân yêu",
                Type = LessonType.Vocabulary,
                ImageUrl = "/images/categories/family.png",
                Words = new List<Word>
                {
                    new Word { Text = "Ông bà", PronunciationGuide = "Ông bà", ImageUrl = "/images/words/grandparents.png", AudioUrl = "/audio/grandparents.mp3" },
                    new Word { Text = "Bố mẹ", PronunciationGuide = "Bố mẹ", ImageUrl = "/images/words/parents.png", AudioUrl = "/audio/parents.mp3" },
                    new Word { Text = "Anh chị", PronunciationGuide = "Anh chị", ImageUrl = "/images/words/siblings.png", AudioUrl = "/audio/siblings.mp3" },
                    new Word { Text = "Em bé", PronunciationGuide = "Em bé", ImageUrl = "/images/words/baby.png", AudioUrl = "/audio/baby.mp3" }
                }
            },
            new Lesson 
            { 
                Title = "Câu ngắn thú vị", 
                Description = "Luyện đọc các câu đơn giản",
                Type = LessonType.Sentence,
                ImageUrl = "/images/categories/sentences.png",
                Words = new List<Word>
                {
                    new Word { Text = "Con mèo ăn cá", PronunciationGuide = "Cơn mèo ăn cá", ImageUrl = "/images/words/cat_eating.png", AudioUrl = "/audio/cat_eat.mp3" },
                    new Word { Text = "Bé đi học", PronunciationGuide = "Bé đi học", ImageUrl = "/images/words/kid_school.png", AudioUrl = "/audio/kid_school.mp3" },
                    new Word { Text = "Hoa hồng rất thơm", PronunciationGuide = "Hoa hồng rất thơm", ImageUrl = "/images/words/flower.png", AudioUrl = "/audio/flower.mp3" },
                    new Word { Text = "Em yêu tiếng Việt", PronunciationGuide = "Em yêu tiếng Việt", ImageUrl = "/images/words/vietnam.png", AudioUrl = "/audio/vietnam.mp3" }
                }
            }
        };

        await context.Lessons.AddRangeAsync(lessons);
        await context.SaveChangesAsync();
    }
}
