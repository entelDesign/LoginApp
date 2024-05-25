using LoginApp.Data;
using LoginApp.Data.Abstract;
using LoginApp.Data.Concreate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LoginContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:sql_connection"]);
});


builder.Services.AddScoped<IUserRepository, EfUserRepository>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/User/Login";
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
