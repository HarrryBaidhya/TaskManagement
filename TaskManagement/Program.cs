using Microsoft.EntityFrameworkCore;
using TaskManagement.Appdbcontext;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.ORMDapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ProductContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductCollegeContextConnection' not found.");
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<ITaskManagment, TaskMangement>();
builder.Services.AddScoped<IProductManagement, ProductManagement>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContextConnection")));


builder.Services.AddHttpContextAccessor();
//Configuring Session Services in ASP.NET Core
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
  // pattern: "{controller=Money}/{action=Index}/{id?}");
 pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
