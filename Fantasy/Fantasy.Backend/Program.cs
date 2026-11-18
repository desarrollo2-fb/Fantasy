using Fantasy.Backend.Data;
using Fantasy.Backend.Respositories.Implementations;
using Fantasy.Backend.Respositories.Interfaces;
using Fantasy.Backend.UnitsOfWork.Implementations;
using Fantasy.Backend.UnitsOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));
builder.Services.AddTransient<SeedDb>();

builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericUnitOfWork<>));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<ICountriesUnitOfWork, CountriesUnitOfWork>();

var app = builder.Build();
await SeedDataAsync(app);

async Task SeedDataAsync(WebApplication app)
{
    await using var scope = app.Services
        .GetRequiredService<IServiceScopeFactory>()
        .CreateAsyncScope();

    var service = scope.ServiceProvider.GetRequiredService<SeedDb>();
    await service.SeedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
app.UseAuthorization();
app.MapControllers();

app.Run();