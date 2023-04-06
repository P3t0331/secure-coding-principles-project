namespace Panbyte.Validators;


public static class ArrayValidator
{
    private const string DEC_REGEX = @"^'[ -~]'$";
    private const string XHEX_REGEX = @"^'\\x[0-9a-fA-F]{2}'$";
    private const string HEX_REGEX = @"^0x[0-9a-fA-F]{2}$";
    private const string BITS_REGEX = @"^0b[01]{1,8}$";

    public static bool isHex(in string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, HEX_REGEX);
    }
    public static bool isCharHex(in string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, XHEX_REGEX);
    }
    public static bool isChar(in string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, DEC_REGEX);
    }
    public static bool isDecimal(in string input)
    {
        return (int.TryParse(input, out int value) && value >= 0 && value <= 255);
    }

    public static bool isBits(in string input)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(input, BITS_REGEX);
    }
}
