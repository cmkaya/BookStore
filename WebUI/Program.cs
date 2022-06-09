using AppCore.DataAccess.Settings;
using Business.Abstracts;
using Business.Concretes;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebUI.Settings;

var builder = WebApplication.CreateBuilder(args);
ConnectionSetting.ConnectionStrings = builder.Configuration.GetConnectionString("BookStoreContext");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Accounts/Login";
        config.AccessDeniedPath = "/Accounts/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        config.SlidingExpiration = true;
    });
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30);
});

builder.Services.AddScoped<DbContext, BookStoreContext>();
builder.Services.AddScoped<BaseAuthorDal, AuthorDal>();
builder.Services.AddScoped<BaseBookDal, BookDal>();
builder.Services.AddScoped<BaseUserDal, UserDal>();
builder.Services.AddScoped<BaseUserDetailDal, UserDetailDal>();
builder.Services.AddScoped<BaseCountryDal, CountryDal>();
builder.Services.AddScoped<BaseCityDal, CityDal>();
builder.Services.AddScoped<BaseOrderDal, OrderDal>();
builder.Services.AddScoped<BaseBookOrderDal, BookOrderDal>();

builder.Services.AddScoped<IBookService, BookManager>();
builder.Services.AddScoped<IAuthorService, AuthorManager>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IAccountService, AccountManager>();
builder.Services.AddScoped<ICountryService, CountryManager>();
builder.Services.AddScoped<ICityService, CityManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();

IConfigurationSection section = builder.Configuration.GetSection(nameof(AppSettings));
section.Bind(new AppSettings());

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();