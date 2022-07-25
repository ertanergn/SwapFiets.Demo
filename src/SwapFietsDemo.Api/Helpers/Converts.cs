namespace SwapFietsDemo.Api.Helpers;

public static class Converts
{
    public static string ToInvariantString(this float input)
    {
        return input.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
    
    public static string ToInvariantString(this double input)
    {
        return input.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }
}