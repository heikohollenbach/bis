namespace BIS.Domain.Entities;

/// <summary>
/// Resource entity representing inventory items/resources
/// </summary>
public class Resource : BaseEntity
{
    public Guid ShipId { get; set; }

    public int ResourceType { get; set; } // ResourceType enum value

    public long Quantity { get; set; }
    
    public long MaxQuantity { get; set; }

    // Navigation properties
    public Ship? Ship { get; set; }
}
