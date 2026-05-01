using Microsoft.EntityFrameworkCore;

namespace Cereal_API.Models;

public class CerealDbContext : DbContext
{
    public CerealDbContext(DbContextOptions<CerealDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cereal> Cereals { get; set; }
    public DbSet<User> Users { get; set; }
}