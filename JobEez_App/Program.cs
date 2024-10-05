using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JobEez_App.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("JobEez_AppContextConnection") ?? throw new InvalidOperationException("Connection string 'JobEez_AppContextConnection' not found.");

builder.Services.AddDbContext<JobEez_AppContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<JobEez_App.Models.JobEezPrimeTechContext>();
builder.Services.AddDefaultIdentity<JobEez_AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<JobEez_AppContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BuildResumes}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
