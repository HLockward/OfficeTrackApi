using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.Data;
using OfficeTrackApi.Entities;
using OfficeTrackApi.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.RespectBrowserAcceptHeader = false;
    options.Filters.Add(new ProducesAttribute("application/json"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// builder.WebHost.UseUrls("http://0.0.0.0:80");

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

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAll");

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
