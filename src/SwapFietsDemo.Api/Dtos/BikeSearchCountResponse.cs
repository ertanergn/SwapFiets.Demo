using System.Text.Json.Serialization;

namespace SwapFietsDemo.Api.Dtos;

public class BikeSearchCountResponse
{
    [JsonPropertyName("non")]
    public int NotStolen { get; set; }
    
    [JsonPropertyName("stolen")]
    public int Stolen { get; set; }
    
    [JsonPropertyName("proximity")]
    public int StolenWithinProximity { get; set; }
}