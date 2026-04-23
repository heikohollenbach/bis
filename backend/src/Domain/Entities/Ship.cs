namespace BIS.Domain.Entities;

/// <summary>
/// Ship entity representing a player's ship
/// </summary>
public class Ship : BaseEntity
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int ShipType { get; set; } // ShipType enum value

    public float Health { get; set; }

    public float MaxHealth { get; set; }

    public float PositionX { get; set; }

    public float PositionY { get; set; }

    public float PositionZ { get; set; }

    public int Level { get; set; } = 1;

    public long Experience { get; set; } = 0;

    // Navigation properties
    public User? User { get; set; }

    public ICollection<Resource> Resources { get; set; } = [];
}
