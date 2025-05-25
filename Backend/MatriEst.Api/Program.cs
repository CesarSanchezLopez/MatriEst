using MatriEst.Api.Core.Interfaces;
using MatriEst.Api.Infrastructure.Data;
using MatriEst.Api.Infrastructure.Repositories;
using MatriEst.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework and SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure repositories
builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();

// Configure services
builder.Services.AddScoped<IInscripcionService, InscripcionService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    await DbInitializer.SeedAsync(context);
}
// Configure the HTTP request pipeline.

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy => policy.AllowAnyOrigin()
//                        .AllowAnyMethod()
//                        .AllowAnyHeader());
//});

//app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
