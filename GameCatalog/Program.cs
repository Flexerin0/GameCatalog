using GameCatalog.Data;
using GameCatalog.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Genres.Any())
    {
        db.Genres.AddRange(
            //new Genre { Name = "Action" },
            //new Genre { Name = "RPG" },
            //new Genre { Name = "Shooter" },
            //new Genre { Name = "Strategy" },
            //new Genre { Name = "Adventure" }
            new Genre { Name = "Экшен" },
            new Genre { Name = "Ролевые игры (RPG)" },
            new Genre { Name = "Шутер" },
            new Genre { Name = "Хоррор" },
            new Genre { Name = "Файтинг" },
            new Genre { Name = "Стратегия" },
            new Genre { Name = "Головоломка" },
            new Genre { Name = "Песочница" },
            new Genre { Name = "Выживание" },
            new Genre { Name = "Визуальная новелла" },
            new Genre { Name = "Лутер-шутер" },
            new Genre { Name = "ММО" },
            new Genre { Name = "Спорт" },
            new Genre { Name = "Соревновательная" },
            new Genre { Name = "Симулятор" }
        );
        db.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
