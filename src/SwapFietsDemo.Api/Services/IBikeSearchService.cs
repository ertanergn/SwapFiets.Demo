using SwapFietsDemo.Api.Dtos;

namespace SwapFietsDemo.Api.Services;

public interface IBikeSearchService
{
    Task<BikeSearchCountResponse> GetBikeStatisticsForLocation(SearchBikesForLocationRequest request, CancellationToken cancellationToken = default);
}