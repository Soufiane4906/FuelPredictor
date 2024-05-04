using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FuelPredictor.Data;
using FuelPredictor.Models;
using System.Configuration;
using FuelPredictor.Models.Users;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FuelPredictorContextConnection") ?? throw new InvalidOperationException("Connection string 'FuelPredictorContextConnection' not found.");


builder.Services.AddDbContext<FuelPredictorContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FuelPredictorContextConnection") ));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<FuelPredictorContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();


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
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var dbContext = services.GetRequiredService<FuelPredictorContext>();
    await ContextSeed.SeedRolesAsync(userManager, roleManager);
    await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);
    await ContextSeed.SeedDataAsync(dbContext);
}




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
