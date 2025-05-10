namespace OfficeTrackApi.Entities;

public class Equipment
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string? SerialNumber { get; set; }

    public int EquipmentTypeId { get; set; }
    public EquipmentType EquipmentType { get; set; } = null!;

    public List<EquipmentMaintenance> EquipmentMaintenances { get; set; } = new();
}
