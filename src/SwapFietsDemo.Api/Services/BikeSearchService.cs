using SwapFietsDemo.Api.Dtos;
using SwapFietsDemo.Api.Exceptions;
using SwapFietsDemo.Api.Helpers;

namespace SwapFietsDemo.Api.Services;

public class BikeSearchService : IBikeSearchService
{
    private readonly HttpClient _client;
    private readonly ILogger _logger;

    public BikeSearchService(HttpClient client, ILogger logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<BikeSearchCountResponse> GetBikeStatisticsForLocation(SearchBikesForLocationRequest request, CancellationToken cancellationToken = default)
    {
        var query = $"search/count?location={request.Latitude.ToInvariantString()},{request.Longitude.ToInvariantString()}&distance={request.ProximityInMiles.ToInvariantString()}";

        try
        {
            var result = await _client.GetFromJsonAsync<BikeSearchCountResponse>(query, cancellationToken);

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            throw new SwapException("Cannot retrieve bike info for given information", e);
        }
    }
}