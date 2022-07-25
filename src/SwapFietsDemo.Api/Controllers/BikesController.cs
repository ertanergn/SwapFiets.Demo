using Microsoft.AspNetCore.Mvc;
using SwapFietsDemo.Api.Dtos;
using SwapFietsDemo.Api.Requests;
using SwapFietsDemo.Api.Responses;
using SwapFietsDemo.Api.Services;

namespace SwapFietsDemo.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BikesController : Controller
{
    private readonly IBikeSearchService _bikeSearchService;
    
    public BikesController(IBikeSearchService bikeSearchService)
    {
        _bikeSearchService = bikeSearchService;
    }
    
    [HttpPost("GetLocationStats")]
    public async Task<GetLocationStatsResponse?> GetLocationStats(GetLocationStatsRequest request, CancellationToken cancellationToken)
    {
        var searchRequest = new SearchBikesForLocationRequest()
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Proximity = request.Proximity,
        };

        var response = await _bikeSearchService.GetBikeStatisticsForLocation(searchRequest, cancellationToken);

        return response == null ? null : new GetLocationStatsResponse()
        {
            NotStolen = response.NotStolen,
            Stolen = response.Stolen,
            StolenWithinProximity = response.StolenWithinProximity
        };
    }
}