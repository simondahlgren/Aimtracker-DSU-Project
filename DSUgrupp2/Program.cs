using DSUgrupp2.Data;
using DSUgrupp2.Infrastructure;
using DSUgrupp2.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connectionPostgres = builder.Configuration["ConnectionStrings:Default"];
string connectionWeatherApi = builder.Configuration["ApiConnectionStrings:EndpointWeather"];
string connectionApiG7 = builder.Configuration["ApiConnectionStrings:EndpointShootingG7"];
string connectionApiG8 = builder.Configuration["ApiConnectionStrings:EndpointShootingG8"];
//string connectionSQLite = builder.Configuration["ConnectionString:Develop"];
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


//builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite(connectionSQLite)); // Hur man ansluter till sqllite databas (DBeaver) Pirater/Hockeyclubar
//builder.Services.AddScoped<IDbRepository, DbRepository>();

//builder.Services.AddDbContext<LoginDbContext>( 
//   options => options.UseSqlite(connectionSQLite));

//builder.Services.AddDbContext<LoginDbContext>(
//   options => options.UseNpgsql(connectionPostgres));

builder.Services.AddDbContext<AppDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
    o => o.SetPostgresVersion(new Version(9, 5))));

builder.Services.AddDbContext<LoginDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Default"),
    o => o.SetPostgresVersion(new Version(9, 5))));



builder.Services.AddScoped<IDbRepository, DbRepository>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>() 
 .AddEntityFrameworkStores<LoginDbContext>();

builder.Services.Configure<IdentityOptions>(options => 
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
});

builder.Services.AddScoped<IApiRepository, ApiRepository>();
builder.Services.AddScoped<IApiClient, ApiClient>();


//builder.Services.AddIdentity<IdentityUser, IdentityRole>() // Hur man kan lägga till en användare
// .AddEntityFrameworkStores<LoginDbContext>();
//builder.Services.Configure<IdentityOptions>(options => // Hur man ställer in regler på ett lösenord
//{
//    options.Password.RequireDigit = false;
//    options.Password.RequireLowercase = false;
//    options.Password.RequireUppercase = false;
//    options.Password.RequiredLength = 5;
//    options.Password.RequireNonAlphanumeric = false;
//});


//builder.Services.AddScoped<IDbRepository, DbRepository>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
