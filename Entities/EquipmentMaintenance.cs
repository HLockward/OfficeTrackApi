namespace OfficeTrackApi.Entities;

public class EquipmentMaintenance
{
    public int EquipmentId { get; set; }
    public Equipment Equipment { get; set; } = null!;

    public int MaintenanceTaskId { get; set; }
    public MaintenanceTask MaintenanceTask { get; set; } = null!;
}
