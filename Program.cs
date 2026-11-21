using Microsoft.EntityFrameworkCore;
using wt_lab5_5mvc_leshkevich.Data;

var builder = WebApplication.CreateBuilder(args);

var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

string connectionString;
if (!string.IsNullOrWhiteSpace(rawConnectionString) &&
    (rawConnectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
     rawConnectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)))
{
    var uri = new Uri(rawConnectionString);
    var userInfo = uri.UserInfo.Split(':', 2);
    var username = Uri.UnescapeDataString(userInfo[0]);
    var password = userInfo.Length > 1 ? Uri.UnescapeDataString(userInfo[1]) : "";
    var database = uri.AbsolutePath.TrimStart('/');
    var portDb = uri.Port > 0 ? uri.Port : 5432;

    connectionString =
        $"Host={uri.Host};Port={portDb};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
}
else
{
    connectionString = rawConnectionString
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
}

// Регистрация AppDbContext с провайдером PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Стандартная регистрация Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Применяем миграции к PostgreSQL при старте приложения
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

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