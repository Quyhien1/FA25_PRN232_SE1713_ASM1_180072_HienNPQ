using Microsoft.AspNetCore.Authentication.Cookies;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ;
using OEMEVWarrantyManagementSystem.Service.HienNPQ;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext (ensure connection string "DefaultConnection" exists)
builder.Services.AddDbContext<FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IBookingHienNpqService, BookingHienNpqService>();
builder.Services.AddScoped<SystemUserAccountService>(); // if already there keep it
builder.Services.AddScoped<SystemUserAccountRepository>();

// Auth (keep whatever you already have)
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Account/Login";
        o.AccessDeniedPath = "/Account/Forbidden";
        o.ExpireTimeSpan = TimeSpan.FromHours(2);
        o.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"); // keep your existing default

app.Run();
