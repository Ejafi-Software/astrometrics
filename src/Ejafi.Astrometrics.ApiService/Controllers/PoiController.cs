using Ejafi.Astrometrics.ApiService.Data;
using Ejafi.Astrometrics.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ejafi.Astrometrics.ApiService.Controllers;

[Controller]
[Route("/pois")]
public class PoiController(IPoiRepository poiRepository) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePoi([FromBody] PointOfInterest poi)
    {
        if (!poi.IsValid())
        {
            return BadRequest("Name must have 2 or more characters");
        }

        if (await poiRepository.ExistsAsync(poi))
        {
            return Conflict();
        }

        await poiRepository.AddAsync(poi);
        return Ok();
    }

    [HttpGet]
    public IActionResult ListPois()
    {
        return Ok(poiRepository.ListAll());
    }

    [HttpPost("filter")]
    public IActionResult ListPois([FromBody] PoiFilter filter)
    {
        return Ok(poiRepository.ListWithFilter(filter));
    }
}
