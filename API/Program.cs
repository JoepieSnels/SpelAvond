using IndividueleCSharpProject.Infrastructure;
using IndividueleCSharpProject.DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

// Configureer de HTTP request pipeline
app.MapControllers();

app.Run();
