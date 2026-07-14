using LexiconUniversity2026.Persistence;
using LexiconUniversity2026.Persistence.Data;
using LexiconUniversity2026.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<LexiconUniversityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LexiconUniversityContext")
?? throw new InvalidOperationException("Connection string 'LexiconUniversityContext' not found!")));

builder.Services.AddScoped<IGetCoursesService, GetCoursesService>(); 

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}else
{
    await app.SeedDataAsync(); 
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
