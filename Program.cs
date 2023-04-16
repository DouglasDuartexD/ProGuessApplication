using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProGuessApplication.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProGuessApplicationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProGuessApplicationContext") ?? throw new InvalidOperationException("Connection string 'ProGuessApplicationContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
//Adiciona ao builder o esquema de autenticação e cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(Option =>
    {
        Option.LoginPath = "/Home/Login";
        Option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
