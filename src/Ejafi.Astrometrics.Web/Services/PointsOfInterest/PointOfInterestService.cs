using Ejafi.Astrometrics.Shared;
using Radzen;

namespace Ejafi.Astrometrics.Web.Services.PointsOfInterest;

public sealed class PointOfInterestService(
    AstrometricsApiClient client,
    NotificationService notificationService) : IPointsOfInterestService
{
    public PointOfInterest DistanceTarget { get; private set; } = PointOfInterest.SagittariusA;
    public PoiFilter CurrentFilter { get; private set; } = new();

    public event Func<PointOfInterest, Task>? PoiAddedAsync;
    public event Func<PoiFilter, Task>? PoiFilterChangedAsync;
    public event Func<Task>? PoiFilterClearedAsync;
    public event Action<PointOfInterest>? DistanceTargetChanged;

    public Task<IEnumerable<PointOfInterest>> GetAllPoisAsync() => client.GetPoisAsync();
    public Task<IEnumerable<PointOfInterest>> GetFilteredPoisAsync(PoiFilter filter) => client.GetPoisAsync(filter);
    public async Task AddPoiAsync(PointOfInterest poi)
    {
        var response = await client.AddPoiAsync(poi);
        if (!response.Success)
        {
            notificationService.Notify(
                new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = response.Message,
                    CloseOnClick = true
                }
            );

            return;
        }

        notificationService.Notify(
            new NotificationMessage
            {
                Severity = NotificationSeverity.Success,
                Summary = $"{poi.Name} Created",
                Detail = response.Message,
                CloseOnClick = true
            }
        );

        PoiAddedAsync?.Invoke(poi);
    }
    public void SetDistanceTarget(PointOfInterest poi)
    {
        DistanceTarget = poi;
        DistanceTargetChanged?.Invoke(DistanceTarget);
    }
    public void ClearDistanceTarget()
    {
        DistanceTarget = PointOfInterest.SagittariusA;
        DistanceTargetChanged?.Invoke(DistanceTarget);
    }
    public void SetFilter(PoiFilter filter)
    {
        CurrentFilter = filter;
        PoiFilterChangedAsync?.Invoke(filter);
    }
    public void ClearFilter()
    {
        CurrentFilter = new PoiFilter();
        PoiFilterClearedAsync?.Invoke();
    }
}
