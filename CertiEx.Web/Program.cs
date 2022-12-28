using System.Globalization;
using CertiEx.Business;
using CertiEx.Dal;
using CertiEx.Business.Concrete;
using CertiEx.Dal.DbInit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();

// Transistents Services
builder.Services.AddTransient<IDbInit, DbInit>();
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Scoped Services
builder.Services.AddBusinessServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//SeedsDatabase
using var scope = app.Services.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInit>();
dbInitializer.Initialize();
//!endseed

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
