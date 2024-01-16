using Microsoft.EntityFrameworkCore;
using WebAppDemoStudent.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    var connectionString =
   builder.Configuration.GetConnectionString("MySqlCon");
    options.UseMySql(connectionString,
   ServerVersion.AutoDetect(connectionString));
});

/*builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyDbContext>();*/

builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
