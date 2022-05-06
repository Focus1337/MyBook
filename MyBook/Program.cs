using System.IO.Compression;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using MyBook.DataAccess;
using MyBook.Entity;
using MyBook.Entity.Identity;
using MyBook.Hubs;
using MyBook.Middleware;
using MyBook.Services.EmailServices;

var builder = WebApplication.CreateBuilder(args);

// Email service
builder.Services.AddSingleton(builder.Configuration.
    GetSection("EmailConfiguration").Get<EmailConfiguration>());
builder.Services.AddScoped<IEmailService,EmailService>();

builder.Services.AddControllersWithViews();
// Database context

var provider = builder.Configuration.GetValue("Provider", "Mssql");

builder.Services.AddDbContext<ApplicationContext>(
    options => _ = provider switch
    {
        "Pgsql" => options.UseNpgsql(
            builder.Configuration.GetConnectionString("sqlConnection"),
            x => x.MigrationsAssembly("PostgresMigrations")),

        "Mssql" => options.UseSqlServer(
            builder.Configuration.GetConnectionString("sqlConnection"),
            x => x.MigrationsAssembly("SqlServerMigrations")),

        _ => throw new Exception($"Unsupported provider: {provider}")
    });

//
// if (builder.Environment.IsDevelopment())
//     builder.Services.AddDbContext<ApplicationContext>(opts =>
//     {
//         opts.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection"),
//             x => x.MigrationsAssembly("PostgresMigrations"));
//     });
// else
//     builder.Services.AddDbContext<ApplicationContextMssql>(opts =>
//     {
//         opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"),
//             x => x.MigrationsAssembly("MssqlMigrations"));
//     });

// Identity
builder.Services.AddIdentity<User, Role>(option=>option.SignIn.RequireConfirmedEmail=true)
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
        {
            options.LoginPath = new PathString("/Auth/Login");
        }
    );

// SignalR
builder.Services.AddSignalR();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policyBuilder =>
        {
            // policyBuilder.WithOrigins("https://mybook.somee.com");
            policyBuilder.AllowAnyOrigin();
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
        });
});
// сжатие ответов
builder.Services.AddResponseCompression(options=>options.EnableForHttps = true);
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = new PathString("/Home/AccessDenied");
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using (var scope = app.Services.CreateScope())
{
    #region migrations

    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();

    #endregion

    #region roles

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    if (!roleManager.Roles.Any())
    {
        await roleManager.CreateAsync(new Role {Name = "Admin"});
        await roleManager.CreateAsync(new Role {Name = "User"});
        await roleManager.CreateAsync(new Role {Name = "UserSub"});
    }

    #endregion
}

// сжатие ответов
app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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

// URL Rewriting
var options = new RewriteOptions()
    .AddRedirect("(.*)/$", "$1")                // удаление концевого слеша
    .AddRedirect("(?i)catalog[/]?$", "home") // переадресация с catalog на home
    .AddRedirect("(?i)auth[/]?$", "home"); // переадресация с auth на home
app.UseRewriter(options);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSubscription();

// CORS
app.UseCors("AllowAll");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// SignalR
app.MapHub<ChatHub>("/chat");

// app.MapControllerRoute(
//     name: "ViewBook",
//     pattern: "{controller=Catalog}/{action=BookDetails}/{bookName?}");

app.Run();