using Microsoft.EntityFrameworkCore;
using OfficeTrackApi.Entities;

namespace OfficeTrackApi.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<ApiDbContext>() ?? throw new InvalidOperationException("ApiDbContext is not registered in the service provider.");
        SeedData(context);
    }

    private static void SeedData(ApiDbContext context)
    {
        context.Database.Migrate();

        if (context.EquipmentTypes.Any())
        {
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var equipmentTypes = new List<EquipmentType>(){
            new() {
                Description = "Laptop"
            },
            new() {
                Description = "Desktop"
            },
            new() {
                Description = "Printer"
            },
            new() {
                Description = "Monitor"
            },
        };
        context.AddRange(equipmentTypes);

        context.SaveChanges();
    }
}

