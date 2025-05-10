namespace OfficeTrackApi.Entities;

public class MaintenanceTask
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public List<EquipmentMaintenance> EquipmentMaintenances { get; set; } = new();
}
