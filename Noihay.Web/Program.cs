using Microsoft.EntityFrameworkCore;
using Noihay.DataAccessLayer;
using Noihay.DataAccessLayer.Interfaces;
using Noihay.DataAccessLayer.Repositories;
using Noihay.Services;
using Noihay.Services.Interfaces;
using Noihay.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NoihayDbContext>(options => 
    options.UseInMemoryDatabase("NoihayDb"));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IGameService, Noihay.Web.Services.GameService>();

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<NoihayDbContext>();
        // context.Database.Migrate(); // Uncomment after adding migrations
        await DataSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();
app.MapControllers();

app.Run();
