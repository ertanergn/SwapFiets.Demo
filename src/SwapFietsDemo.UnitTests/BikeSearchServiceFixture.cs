using System.Net;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using SwapFietsDemo.Api.Dtos;
using SwapFietsDemo.Api.Exceptions;
using SwapFietsDemo.Api.Services;

namespace SwapFietsDemo.UnitTests;

[TestFixture]
public class BikeSearchServiceFixture
{
    private readonly ILogger _logger;
    private readonly MockHttpMessageHandler _mockHttp;
    
    private BikeSearchService _sut;

    private SearchBikesForLocationRequest Request;
    private BikeSearchCountResponse Response;
    
    public BikeSearchServiceFixture()
    {
        _logger = Mock.Of<ILogger>();
        _mockHttp = new MockHttpMessageHandler();
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        Request = new SearchBikesForLocationRequest
        {
            Latitude = 52.377956f,
            Longitude = 4.897070f,
            Proximity = 5
        };
        
        Response = new BikeSearchCountResponse
        {
            Stolen = 115957,
            NotStolen = 744359,
            StolenWithinProximity = 91
        };
    }

    [TearDown]
    public void TearDown()
    {
        _mockHttp.Flush();
    }

    [Test]
    public void GetBikeStatisticsForLocation_throws_exception_when_httpclient_has_error()
    {
        // Arrange
        _mockHttp.Expect("https://bikeindex.org/api/v3/search/count")
            .WithQueryString("location", "52.377956,4.89707")
            .WithQueryString("distance", "3.1068559611866697")
            .Respond(HttpStatusCode.BadRequest);

        var client = _mockHttp.ToHttpClient();
        client.BaseAddress = new Uri("https://bikeindex.org/api/v3/");

        _sut = new BikeSearchService(client, _logger);
        
        //act
        var ex = Assert.ThrowsAsync<SwapException>(async () => await _sut.GetBikeStatisticsForLocation(Request));
        
        //Assert
        Assert.NotNull(ex);
        Assert.That("Cannot retrieve bike info for given information", Is.EqualTo(ex.Message));
        _mockHttp.VerifyNoOutstandingExpectation();
    }
    
    [Test]
    public async Task GetBikeStatisticsForLocation_succeeds()
    {
        // Arrange
        _mockHttp.Expect("https://bikeindex.org/api/v3/search/count")
            .WithQueryString("location", "52.377956,4.89707")
            .WithQueryString("distance", "3.1068559611866697")
            .Respond("application/json", "{\"non\":744359,\"stolen\":115957,\"proximity\":91}");

        var client = _mockHttp.ToHttpClient();
        client.BaseAddress = new Uri("https://bikeindex.org/api/v3/");

        _sut = new BikeSearchService(client, _logger);
        
        //act
        var result = await _sut.GetBikeStatisticsForLocation(Request);
        
        //Assert
        Assert.NotNull(result);
        Assert.That(result.Stolen, Is.EqualTo(Response.Stolen));
        Assert.That(result.NotStolen, Is.EqualTo(Response.NotStolen));
        Assert.That(result.StolenWithinProximity, Is.EqualTo(Response.StolenWithinProximity));
        _mockHttp.VerifyNoOutstandingExpectation();
    }
}