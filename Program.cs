using AMVTravels.Models;
using AMVTravels.Services;
using AMVTravels.Services.Contract;
using AMVTravels.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AMVTravelContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmvString"));
});

builder.Services.AddScoped<GestorReservas>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Inicio/IniciarSesion";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });


builder.Services.AddControllersWithViews(option =>
{
    option.Filters.Add(
           new ResponseCacheAttribute
           {
               NoStore = true,
               Location = ResponseCacheLocation.None,
           }
        );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("SecutityPolicy", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCors("SecutityPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Inicio}/{action=IniciarSesion}/{id?}");

app.Run();
