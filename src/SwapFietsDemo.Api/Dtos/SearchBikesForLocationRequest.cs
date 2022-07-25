namespace SwapFietsDemo.Api.Dtos;

public class SearchBikesForLocationRequest
{
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public int Proximity { get; set; }

    public double ProximityInMiles => (Proximity / 1.609344);
    public Stolenness Stolenness { get; set; }
}