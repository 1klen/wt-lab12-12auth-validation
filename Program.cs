using Microsoft.EntityFrameworkCore;
using wt_lab5_5mvc_leshkevich.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Регистрация AppDbContext с провайдером PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Стандартная регистрация Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Настройка конвейера запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Обязательно: подключение статических файлов (CSS, JS, Bootstrap) ДО UseRouting
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Маппинг маршрутов для Razor Pages
app.MapRazorPages();

// Получаем порт для локального запуска или деплоя на Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");