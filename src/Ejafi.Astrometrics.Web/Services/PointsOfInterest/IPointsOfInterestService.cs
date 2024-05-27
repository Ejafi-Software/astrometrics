using Ejafi.Astrometrics.Shared;

namespace Ejafi.Astrometrics.Web.Services.PointsOfInterest;

public interface IPointsOfInterestService
{
    public event Func<PointOfInterest, Task>? PoiAddedAsync;
    public event Func<PoiFilter, Task>? PoiFilterChangedAsync;
    public event Func<Task>? PoiFilterClearedAsync;
    public event Action<PointOfInterest>? DistanceTargetChanged;

    public PointOfInterest DistanceTarget { get; }
    public PoiFilter CurrentFilter { get; }

    public Task<IEnumerable<PointOfInterest>> GetAllPoisAsync();
    public Task<IEnumerable<PointOfInterest>> GetFilteredPoisAsync(PoiFilter filter);
    public Task AddPoiAsync(PointOfInterest poi);
    public void SetDistanceTarget(PointOfInterest poi);
    public void ClearDistanceTarget();
    public void SetFilter(PoiFilter filter);
    public void ClearFilter();
}
