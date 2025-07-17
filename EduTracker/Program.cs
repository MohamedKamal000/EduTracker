using EduTracker.Models;
using EduTracker.Models.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


DotNetEnv.Env.Load();


builder.Services.AddDbContext<ApplicationDbContext>
    (o => o.UseMySql(Environment
        .GetEnvironmentVariable("DATABASE_CONNECTIONSTRING"),
        new MySqlServerVersion(new Version(8,0,42))));


builder.Services.AddScoped<StudentHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();