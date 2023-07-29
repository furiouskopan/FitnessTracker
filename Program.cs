using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FitTrackerDbContext>(options =>
{
    var connectionString = "Data Source=DESKTOP-S5ULU19\\SQLEXPRESS;Initial Catalog=FitnessTrackingDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";
    options.UseSqlServer(connectionString);
});

//builder.Services.AddDbContext<FitTrackerDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<FitTrackerDbContext>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ExerciseLog}/{action=LogList}");

app.MapControllerRoute(
    name: "exercise",
    pattern: "{controller=Exercise}/{action=Create}/{id?}");

app.MapControllerRoute(
    name: "exerciseLog",
    pattern: "Exercise/CreateExerciseLog",
    defaults: new { controller = "ExerciseLog", action = "CreateExerciseLog" });


app.MapControllerRoute(
        name: "exerciseLogs",
        pattern: "Exercise/LogList",
        defaults: new { controller = "ExerciseLog", action = "LogList" });

app.MapControllerRoute(
    name: "exerciseLogUpdate",
    pattern: "ExerciseLog/update/{id?}",
    defaults: new { controller = "ExerciseLog", action = "Update" }
);
app.MapControllerRoute(
    name: "exerciseLogDelete",
    pattern: "ExerciseLog/Delete/{id?}",
    defaults: new { controller = "ExerciseLog", action = "Delete" }
);

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=ExerciseLog}/{action=LogList}/{id?}");

//app.MapControllerRoute(
//    name: "exercise",
//    pattern: "{controller=Exercise}/{action=Create}/{id?}");

//app.MapControllerRoute(
//    name: "exerciseLogs",
//    pattern: "ExerciseLog/LogList",
//    defaults: new { controller = "ExerciseLog", action = "LogList" });

//app.MapControllerRoute(
//    name: "exerciseLogCreate",
//    pattern: "ExerciseLog/Create",
//    defaults: new { controller = "ExerciseLog", action = "Create" });

//app.MapControllerRoute(
//    name: "exerciseLogUpdate",
//    pattern: "ExerciseLog/Update/{id?}",
//    defaults: new { controller = "ExerciseLog", action = "Update" });


app.Run();
