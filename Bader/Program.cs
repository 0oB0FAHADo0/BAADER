using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Bader;
using System.Configuration;
using Bader.Models;
using Bader.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//For Connection To  Data Base
builder.Services.AddDbContext<BaaderContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DBCS")));
// For Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/Home/Error";
                options.LoginPath = "/Access/login";
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                //options.LoginPath = "/accounts/ErrorNotLoggedIn";
                //options.LogoutPath = "account/logout";
            });
builder.Services.AddDistributedMemoryCache();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAdmin", p => p.RequireAuthenticatedUser().RequireRole("Admin"));

    //New policies for College Codes

   options.AddPolicy("CollegeCodePolicy15", policy =>
   {
       policy.RequireClaim("CollegeCode", "15");
   });

   options.AddPolicy("CollegeCodePolicy19", policy =>
   {
       policy.RequireClaim("CollegeCode", "19");
   });

});
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.None;
});

builder.Services.AddHealthChecks();
builder.Services.AddRazorPages();

builder.Services.AddScoped<ContentDomain>();
builder.Services.AddScoped<PermissionDomain>();
builder.Services.AddScoped<UserDomain>();// While running don`t forget to type In URL: Users/Create 

builder.Services.AddScoped<SessionDomain>();

builder.Services.AddScoped<CourseDomain>();
builder.Services.AddScoped<RegistrationDomain>();
builder.Services.AddScoped<AttendanceDomain>();
builder.Services.AddScoped<CollegeDomain>();
builder.Services.AddScoped<LevelDomain>();
builder.Services.AddScoped<MajorDomain>();
var app = builder.Build();
// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSession();


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Access}/{action=Login}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Access}/{action=Login}/{id?}");

    endpoints.MapRazorPages();
});



app.Run();