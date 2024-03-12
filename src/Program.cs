using CounTrivia.Services;
using CounTrivia.Services.ChallengeGenerator;
using CounTrivia.Services.CountryProvider;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add entity framework and db context
builder.Services.AddDbContext<CounTriviaDbContext>();

// Add services for dependency injection
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<ICountryProvider, RestApiCountryProvider>();
builder.Services.AddScoped<IChallengeGenerator, ChallengeGenerator>();

// Allow CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost", "http://localhost:80");
            policy.WithHeaders("content-type");
        }
    );
});

var app = builder.Build();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    CounTriviaDbContext dbContext = scope.ServiceProvider.GetRequiredService<CounTriviaDbContext>();

    // Here is the migration executed
    if (dbContext.Database.IsRelational())
    {
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();