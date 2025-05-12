using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.Data;
using OfficeTrackApi.Entities;
using OfficeTrackApi.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Equipment>, Repository<Equipment>>();
builder.Services.AddScoped<IRepository<EquipmentType>, Repository<EquipmentType>>();
builder.Services.AddScoped<IRepository<MaintenanceTask>, Repository<MaintenanceTask>>();
builder.Services.AddScoped<IRepository<EquipmentMaintenance>, Repository<EquipmentMaintenance>>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
