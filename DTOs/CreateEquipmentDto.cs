namespace OfficeTrackApi.DTOs;

public class CreateEquipmentDto
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int EquipmentTypeId { get; set; }
    public string? SerialNumber { get; set; }
}
