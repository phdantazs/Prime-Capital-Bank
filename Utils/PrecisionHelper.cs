namespace PrimeCapitalBank.Utils;

public static class PrecisionHelper
{
    public static decimal TruncateBrl(decimal value)
    {
        return Math.Truncate(value * 100m) / 100m;
    }
    
    public static decimal TruncateBitcoin(decimal value)
    {
        return Math.Truncate(value * 100_000_000m) / 100_000_000m;
    }
}