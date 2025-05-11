using OfficeTrackApi.Entities;

namespace OfficeTrackApi.Repositories;

public interface IUnitOfWork
{
    IRepository<Equipment> Equipment { get; }
    IRepository<EquipmentType> EquipmentTypes { get; }
    IRepository<MaintenanceTask> MaintenanceTasks { get; }
    IRepository<EquipmentMaintenance> EquipmentMaintenances { get; }
    Task SaveAsync();
}
