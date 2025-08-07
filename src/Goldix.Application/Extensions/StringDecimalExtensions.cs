namespace Goldix.Application.Extensions;

public static class StringDecimalExtensions
{
    public static bool IsValidDecimal(this string value)
        => decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out _);

    public static bool IsPositiveDecimal(this string value)
        => decimal.TryParse(value, out var result) && result > 0;

    public static bool HaveCorrectPrecision(this string value)
        => decimal.TryParse(value, out var result) &&
           decimal.GetBits(result)[3] >> 16 <= 2; // Max 2 decimal places
}
