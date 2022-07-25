namespace SwapFietsDemo.Api.Responses;

public class GetLocationStatsResponse
{
    public int NotStolen { get; set; }
    
    public int Stolen { get; set; }
    
    public int StolenWithinProximity { get; set; }
}