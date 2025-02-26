using Blood.Business;
using Blood.infrastructure;
using BloodManagement.ServicesExtensions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Appdbcontext;
using TaskManagement.Interface;
using TaskManagement.Models;
using TaskManagement.ORMDapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ProductContextConnection") ?? throw new InvalidOperationException("Connection string 'ProductCollegeContextConnection' not found.");
//builder.Services.AddSingleton<DapperContext>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddScoped<ITaskManagment, TaskMangement>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductContextConnection")));
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddApplicationServices();
builder.Services.AddSingleton<RepositoryDao>();
builder.Services.AddAutoMapper(typeof(Program));


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.MapHub<ChatHub>("/chatHub");
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
