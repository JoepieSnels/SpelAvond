using IndividueleCSharpProject.Infrastructure;
using IndividueleCSharpProject.DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Laad appsettings.API.json
builder.Configuration
    .AddJsonFile("appsettings.API.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Voeg de databasecontext toe aan de container
builder.Services.AddDbContext<GamenightDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Voeg de repository toe aan de DI-container
builder.Services.AddScoped<IGameNightRepository, GamenightRepositoryEF>();

// Voeg de controllers toe aan de container en configureer de JSON-instellingen
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configureer de JSON-seriële opties om referentiehandling te ondersteunen
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    // Optioneel: stel andere instellingen in zoals dieptebeperkingen
    options.JsonSerializerOptions.MaxDepth = 64; // Bijv. om diep geneste objecten te ondersteunen
});

// Voeg Swagger toe
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configureer de HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    // Gebruik Swagger en stel een standaardpagina in
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpelletjesAvond API v1");
        c.RoutePrefix = string.Empty; // Stelt Swagger in op de root (/)
    });
}

// Voeg de controllers toe aan de request pipeline
app.MapControllers();

// Redirect naar Swagger bij root toegang
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.Run();
