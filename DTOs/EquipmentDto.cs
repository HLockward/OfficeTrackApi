using System;

namespace OfficeTrackApi.DTOs;

public class EquipmentDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int EquipmentTypeId { get; set; }
    public string EquipmentTypeDescription { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; }
    public string? SerialNumber { get; set; }
}
