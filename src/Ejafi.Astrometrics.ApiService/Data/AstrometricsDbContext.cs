using Ejafi.Astrometrics.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ejafi.Astrometrics.ApiService.Data;

public sealed class AstrometricsDbContext : DbContext
{
    public DbSet<PointOfInterest> PointsOfInterest { get; set; }

    public AstrometricsDbContext(DbContextOptions<AstrometricsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PointOfInterest>()
            .ToContainer(nameof(PointsOfInterest))
            .HasPartitionKey(poi => poi.Id)
            .HasNoDiscriminator();
    }
}
