using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using JobEez_App.Models;
using Microsoft.AspNetCore.Identity.UI.Services;


var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("JobEez_AppContextConnection")
                       ?? throw new InvalidOperationException("Connection string 'JobEez_AppContextConnection' not found.");

// Register the single DbContext
builder.Services.AddDbContext<JobEezPrimeTechContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddIdentity<AspNetUser, IdentityRole>()
    .AddEntityFrameworkStores<JobEezPrimeTechContext>()
    .AddDefaultTokenProviders();

// Add other services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<IEmailSender, DummyEmailSender>();

var app = builder.Build();



// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BuildResumes}/{action=Index}/{id?}");


app.MapRazorPages();
// Call the SeedData method
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.Run();
