using Ejafi.Astrometrics.Shared;
using System.Net;

namespace Ejafi.Astrometrics.Web
{
    public class AstrometricsApiClient(HttpClient client, ILogger<AstrometricsApiClient> logger)
    {
        private bool _apiAvailable = true;

        public async Task<ApiResponse> AddPoiAsync(PointOfInterest poi)
        {
            if (!_apiAvailable)
            {
                return new ApiResponse(false, "The API service is not available");
            }

            try
            {
                var response = await client.PostAsJsonAsync("pois", poi);
                response.EnsureSuccessStatusCode();
                return new ApiResponse(true, "Point of Interest added successfully.");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                return new ApiResponse(false, $"Invalid data: {ex.Message}");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                return new ApiResponse(false, "Conflict: A similar point of interest already exists.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a Point of Interest");
                return new ApiResponse(false, $"An error occurred: {ex.Message}");
            }
        }

        public async Task<IEnumerable<PointOfInterest>> GetPoisAsync(PoiFilter? filter = null)
        {
            if (!_apiAvailable)
            {
                return Array.Empty<PointOfInterest>();
            }

            try
            {
                HttpResponseMessage response;
                if (filter is null)
                {
                    response = await client.GetAsync("pois");
                }
                else
                {
                    response = await client.PostAsJsonAsync("pois/filter", filter);
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IEnumerable<PointOfInterest>>() ?? Array.Empty<PointOfInterest>();
            }
            catch (HttpRequestException ex)
            {
                logger.LogError(ex, "HTTP request error: {StatusCode}", ex.StatusCode);
                _apiAvailable = false;
                return Array.Empty<PointOfInterest>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving Points of Interest");
                return Array.Empty<PointOfInterest>();
            }
        }
    }
}
