using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using MovieApp.Data;
using MovieApp.Models;
using Microsoft.Extensions.DependencyInjection;
using MovieApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbConnectionString")));

// Add authentication configuration here
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Home/Login";
    options.LogoutPath = "/Home/Logout"; 
});

builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Movies}/{id?}");
app.MapControllerRoute(
    name: "movies",
    pattern: "{controller=Home}/{action=Movie}/{id?}");
app.MapControllerRoute(
    name: "categories",
    pattern: "{controller=Home}/{action=Category}/{id?}");
app.MapControllerRoute(
    name: "profile",
    pattern: "Profile",
    defaults: new { controller = "Home", action = "Profile" });
app.MapControllerRoute(
    name: "posts",
    pattern: "{controller=Post}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "message",
    pattern: "Message/{action}/{id?}",
    defaults: new { controller = "Message" });
app.Run();
