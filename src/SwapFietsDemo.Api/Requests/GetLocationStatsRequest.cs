namespace SwapFietsDemo.Api.Requests;

public class GetLocationStatsRequest
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public int Proximity { get; set; }
}