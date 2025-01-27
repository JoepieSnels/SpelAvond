using IndividueleCSharpProject.DomainServices.Repositories;
using IndividueleCSharpProject.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Zorg ervoor dat je de juiste verbinding hebt
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Voeg DbContexts toe voor zowel Gamenight als Identity
builder.Services.AddDbContext<GamenightDBContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<IdentityContext>(
    options =>
    {
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.MigrationsHistoryTable("EFMigrationHistory", IdentityContext.SQL_Schema);
        });
    });

// Voeg Identity toe voor gebruikersauthenticatie
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    // Password settings, for now I won't add to much security, it only needs to be 5 characters long
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<IdentityContext>()
.AddDefaultTokenProviders();

// Voeg je repositories toe
builder.Services.AddScoped<IGameNightRepository, GamenightRepositoryEF>();
builder.Services.AddScoped<IGameRepository, GamesRepositoryEF>();
builder.Services.AddScoped<IPersonsRepository, PersonRepositoryEF>();
builder.Services.AddScoped<IReviewsRepository, ReviewRepositoryEF>();

// Voeg controllers met views toe
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configureer HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Zorg ervoor dat authenticatie en autorisatie goed werken
app.UseAuthentication();
app.UseAuthorization();

// Stel je standaard route in
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
