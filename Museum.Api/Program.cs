using Microsoft.EntityFrameworkCore;
using Museum.BusinessLogic.BLs.Contracts;
using Museum.BusinessLogic.BLs.Implementation;
using Museum.DataAccess.Context;
using Museum.DataAccess.Repositories.Contracts;
using Museum.DataAccess.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MuseumContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<IArtworksBL, ArtworksBL>();
builder.Services.AddScoped<IArtworksRepo, ArtworksRepo>();

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
