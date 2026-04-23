namespace BIS.Common.Constants;

/// <summary>
/// Game-wide constants
/// </summary>
public static class BISConstants
{
    // Network & Performance
    public const int GAME_TICK_RATE = 20; // 20 ticks per second = 50ms per tick
    public const int TICK_INTERVAL_MS = 50;

    // World & Zones
    public const int ZONE_OF_INTEREST_RADIUS = 100; // km
    public const int MAX_PLAYERS_PER_ZONE = 500;

    // Starting Resources
    public const int STARTING_CREDITS = 1000;
    public const int STARTING_IRON = 100;
    public const int STARTING_COPPER = 50;

    // Ship Stats
    public const float SHUTTLE_SPEED = 50f; // units/second
    public const float SHUTTLE_HEALTH = 100f;
    public const int SHUTTLE_INVENTORY_SLOTS = 10;

    // Combat
    public const int COMBAT_TICK_RATE = 20; // Updates combat 20 times per second
    public const float BASE_DAMAGE = 10f;

    // Chat
    public const int MAX_MESSAGE_LENGTH = 500;
    public const int CHAT_RATE_LIMIT = 5; // messages per second
}
