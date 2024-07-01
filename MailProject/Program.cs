using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using MailProject.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MailContext>();

builder.Services.AddScoped<IMessagesDal, EfMessagesDal>();
builder.Services.AddScoped<IMessagesService,MessagesManager>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<MailContext>().AddErrorDescriber<CustomIdentityValidator>().AddDefaultTokenProviders(); 

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UserManager<AppUser>>();

builder.Services.AddAuthentication();

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
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
