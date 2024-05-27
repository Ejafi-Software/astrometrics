using Ejafi.Astrometrics.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ejafi.Astrometrics.ApiService.Data;

public class PoiRepository(AstrometricsDbContext dbContext) : IPoiRepository
{
    public async Task AddAsync(PointOfInterest poi)
    {
        if (!await ExistsAsync(poi))
        {
            await dbContext.PointsOfInterest.AddAsync(poi);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(PointOfInterest poi)
    {
        var gx = poi.Coordinate.Galactic.X;
        var gy = poi.Coordinate.Galactic.Y;
        var gz = poi.Coordinate.Galactic.Z;
        var lx = poi.Coordinate.Local.X;
        var ly = poi.Coordinate.Local.Y;
        var lz = poi.Coordinate.Local.Z;
        var existingPoi = dbContext.PointsOfInterest.Where(other =>
            other.Coordinate.Galactic.X == gx &&
            other.Coordinate.Galactic.Y == gy &&
            other.Coordinate.Galactic.Z == gz &&
            other.Coordinate.Local.X == lx &&
            other.Coordinate.Local.Y == ly &&
            other.Coordinate.Local.Z == lz);
        return await existingPoi.FirstOrDefaultAsync() is not null;
    }


    public IEnumerable<PointOfInterest> ListAll() => dbContext.PointsOfInterest.AsEnumerable();

    public IEnumerable<PointOfInterest> ListWithFilter(PoiFilter filter)
    {
        var query = dbContext.PointsOfInterest.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }

        if (filter.Type != PoiType.All)
        {
            query = query.Where(p => p.Type == filter.Type);
        }

        if (filter.IncludePopulated)
        {
            query = query.Where(p => p.IsPopulated);
        }

        if (filter.Distance >= long.MaxValue) return query.AsEnumerable();

        var list = query
            .Include(pointOfInterest => pointOfInterest.Coordinate)
            .ToList();
        return list.Where(p => p.Coordinate
                .CalculateDistance(filter.CenterPoint)
                .Magnitude <= filter.Distance
        );
    }
}
