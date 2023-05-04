using TripRoutes.Data;
using TripRoutes.Domain;
using TripRoutes.Domain.Interfaces;
using TripRoutes.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(opt => builder.Configuration.GetSection("ConfigFile").Get<ConfigFile>());
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddTransient<IRouteRepository, RouteRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
