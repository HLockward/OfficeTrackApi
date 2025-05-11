namespace OfficeTrackApi.DTOs;

public class EquipmentMaintenanceDto
{
    public int EquipmentId { get; set; }
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string EquipmentType { get; set; } = null!;
    public List<string> MaintenanceTasks { get; set; } = [];
}
