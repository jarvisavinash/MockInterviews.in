using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using MockInterviews.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure authentication services
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme to cookie authentication
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // For Google Authentication
})
.AddCookie(options =>
{
    // Set the login path for unauthenticated users
    options.LoginPath = "/Account/Login";
    // Optionally, you can set the logout path, access denied path, etc.
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
})
.AddGoogle(options =>
{
    options.ClientId = "1051126877348-p94s8drl9bbdmc6uv1m2v1dkkvr25ugp.apps.googleusercontent.com";
    options.ClientSecret = "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";
    options.CallbackPath = "/signin-google"; // Match the redirect URI
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Ensure the middleware is in the correct order: Authentication must come before Authorization
app.UseAuthentication();  // This middleware enables authentication for the app
app.UseAuthorization();   // This middleware enables authorization (ensuring the user is authorized)

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
