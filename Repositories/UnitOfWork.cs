using OfficeTrackApi.Entities;

namespace OfficeTrackApi.Repositories;

public class UnitOfWork(ApiDbContext context,
                  IRepository<Equipment> equipmentRepository,
                  IRepository<EquipmentType> equipmentTypeRepository,
                  IRepository<MaintenanceTask> maintenanceTaskRepository,
                  IRepository<EquipmentMaintenance> equipmentMaintenanceRepository) : IUnitOfWork
{
    private readonly ApiDbContext _context = context;

    public IRepository<Equipment> Equipment { get; } = equipmentRepository;
    public IRepository<EquipmentType> EquipmentTypes { get; } = equipmentTypeRepository;
    public IRepository<MaintenanceTask> MaintenanceTasks { get; } = maintenanceTaskRepository;
    public IRepository<EquipmentMaintenance> EquipmentMaintenances { get; } = equipmentMaintenanceRepository;

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
