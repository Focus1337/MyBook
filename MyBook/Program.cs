using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Entity.Identity;
using MyBook.Middleware;
using MyBook.Services.EmailServices;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(builder.Configuration.
    GetSection("EmailConfiguration").Get<EmailConfiguration>());

builder.Services.AddScoped<IEmailService,EmailService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddIdentity<User, Role>(option=>option.SignIn.RequireConfirmedEmail=true)
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Auth/Login");
        }
    );

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    switch (context.Response.StatusCode)
    {
        case 404:
            context.Request.Path = "/Home/PageNotFound";
            await next();
            break;
        case 403:
            context.Request.Path = "/Home/AccessDenied";
            await next();
            break;
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSubscription();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();