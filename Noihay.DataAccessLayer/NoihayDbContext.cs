using Microsoft.EntityFrameworkCore;
using Noihay.BusinessObject;

namespace Noihay.DataAccessLayer;

public class NoihayDbContext : DbContext
{
    public NoihayDbContext(DbContextOptions<NoihayDbContext> options) : base(options)
    {
    }

    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<UserProgress> UserProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Lesson -> Word relationship
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.Words)
            .WithOne(w => w.Lesson)
            .HasForeignKey(w => w.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed some categories/lessons if needed (Optional, better in a separate Seeder)
    }
}
