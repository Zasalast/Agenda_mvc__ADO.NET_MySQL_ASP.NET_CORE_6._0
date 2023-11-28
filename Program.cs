using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Data.Repositories;
using Agenda_mvc__ADO.NET_MySQL_ASP.NET_CORE_6._0.Repositories;

var builder = WebApplication.CreateBuilder(args);

 

// Add services to the container.
 

 





// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<MySQLSettings>(
     builder.Configuration.GetSection("MySQLSettings"));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddTransient<AgendaRepository>();
builder.Services.AddTransient<AgendamientoRepository>();
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) .AddCookie(o => { o.LoginPath = "/Agenda/Login";  });
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
//.UseAuthorization();

 
// Map routes for Agendar and Agendamiento controllers and their actions
app.MapControllerRoute(
    name: "AgendamientoDetails",
    pattern: "Agendamiento/Details/{id}",
    defaults: new { controller = "Agendamiento", action = "Details" });

app.MapControllerRoute(
    name: "AgendamientoCreate",
    pattern: "Agendamiento/Create",
    defaults: new { controller = "Agendamiento", action = "Create" });

app.MapControllerRoute(
    name: "AgendamientoEdit",
    pattern: "Agendamiento/Edit/{id}",
    defaults: new { controller = "Agendamiento", action = "Edit" });

app.MapControllerRoute(
    name: "AgendamientoList",
    pattern: "Agendamiento/List",
    defaults: new { controller = "Agendamiento", action = "List" });

app.MapControllerRoute(
    name: "AgendamientoDelete",
    pattern: "Agendamiento/Delete/{id}",
    defaults: new { controller = "Agendamiento", action = "Delete" });

app.MapControllerRoute(
    name: "AgendamientoIndex",
    pattern: "Agendamiento",
    defaults: new { controller = "Agendamiento", action = "Index" });

// Map routes for Agenda controller and its actions
app.MapControllerRoute(
    name: "AgendaDetails",
    pattern: "Agenda/Details/{id}",
    defaults: new { controller = "Agenda", action = "Details" });

app.MapControllerRoute(
    name: "AgendaCreate",
    pattern: "Agenda/Create",
    defaults: new { controller = "Agenda", action = "Create" });

app.MapControllerRoute(
    name: "AgendaEdit",
    pattern: "Agenda/Edit/{id}",
    defaults: new { controller = "Agenda", action = "Edit" });

app.MapControllerRoute(
    name: "AgendaList",
    pattern: "Agenda/List",
    defaults: new { controller = "Agenda", action = "List" });

app.MapControllerRoute(
    name: "AgendaDelete",
    pattern: "Agenda/Delete/{id}",
    defaults: new { controller = "Agenda", action = "Delete" });

app.MapControllerRoute(
    name: "AgendaIndex",
    pattern: "Agenda",
    defaults: new { controller = "Agenda", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
