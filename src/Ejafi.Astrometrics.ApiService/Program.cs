using Ejafi.Astrometrics.ApiService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile("appsettings.local.json")
    .AddEnvironmentVariables();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddCosmosDbContext<AstrometricsDbContext>("cosmosdb", "astrometrics");

// Add services to the container.
builder.Services.AddScoped<IPoiRepository, PoiRepository>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.MapDefaultEndpoints();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
