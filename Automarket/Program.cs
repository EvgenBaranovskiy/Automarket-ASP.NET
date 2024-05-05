using Automarket.DAL;
using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Service.Implementations;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

		//Отримати рядок підключення до бази даних
		var connection = builder.Configuration.GetConnectionString("DefaultConnection");

		//Налаштування автентифікації
		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => options.LoginPath = "/Account/Login");
        builder.Services.AddAuthorization();

		//Додавання сервісів до контейнера
		builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(connection); });
        builder.Services.AddScoped<IFileRepository, FileRepository>(serviceProvider =>
        {
            var hostingEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var wwwrootPath = hostingEnvironment.WebRootPath;
            return new FileRepository(wwwrootPath);
        });
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();

        var app = builder.Build();

		//Налаштування конвеєра HTTP-запиту
		if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Car}/{action=GetPage}/{number?}");
        app.Run();
    }
}