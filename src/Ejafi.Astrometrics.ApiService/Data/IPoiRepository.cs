using Ejafi.Astrometrics.Shared;

namespace Ejafi.Astrometrics.ApiService.Data;

public interface IPoiRepository
{
    public Task AddAsync(PointOfInterest poi);
    public Task<bool> ExistsAsync(PointOfInterest poi);
    public IEnumerable<PointOfInterest> ListAll();

    public IEnumerable<PointOfInterest> ListWithFilter(PoiFilter filter);
}
