using IndividueleCSharpProject.DomainServices.Repositories;
using IndividueleCSharpProject.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Zorg ervoor dat je de juiste verbinding hebt
builder.Services.AddDbContext<GamenightDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Voeg Identity toe
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
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

// HTTP request pipeline configureren
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Stel je standaard route in
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
