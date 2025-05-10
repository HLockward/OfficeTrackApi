namespace OfficeTrackApi.Entities;
public class EquipmentType
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;

    public List<Equipment> Equipment { get; set; } = new();
}