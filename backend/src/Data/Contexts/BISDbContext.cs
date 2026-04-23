using Microsoft.EntityFrameworkCore;
using BIS.Domain.Entities;

namespace BIS.Data.Contexts;

/// <summary>
/// Entity Framework Core DbContext for the game database
/// </summary>
public class BISDbContext(DbContextOptions<BISDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Ship> Ships { get; set; }
    
    public DbSet<Resource> Resources { get; set; }
}
