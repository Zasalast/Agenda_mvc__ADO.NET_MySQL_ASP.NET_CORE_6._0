using Microsoft.AspNetCore.Authentication.Cookies;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<MySQLSettings>(
     builder.Configuration.GetSection("MySQLSettings"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddTransient<AgendaRepository>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Persona/Login";
    });
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
