namespace Panbyte.Validators;


public static class ArrayValidator
{
    private const string DEC_REGEX = @"^'[ -~]'$";
    private const string XHEX_REGEX = @"^'\\x[0-9a-fA-F]{2}'$";
    private const string HEX_REGEX = @"^0x[0-9a-fA-F]{2}$";
    private const string BITS_REGEX = @"^0b[01]{1,8}$";

    private const string BRACKET_REGEX = @"'[\[\]\{\}\(\)]'|''";

    public static void CheckCorrectNesting(in string input)
    {
        string filteredInput = extractBrackets(input);

        int curlyOpenCount = 0;
        int squareOpenCount = 0;
        int roundOpenCount = 0;

        foreach (char c in filteredInput)
        {
            switch (c)
            {
                case '{':
                    curlyOpenCount++;
                    break;
                case '}':
                    curlyOpenCount--;
                    break;
                case '[':
                    squareOpenCount++;
                    break;
                case ']':
                    squareOpenCount--;
                    break;
                case '(':
                    roundOpenCount++;
                    break;
                case ')':
                    roundOpenCount--;
                    break;
                default:
                    throw new Exception("Unexpected character: " + c);
            }
        }


        if (curlyOpenCount != 0 || squareOpenCount != 0 || roundOpenCount != 0)
        {
            throw new FormatException("Input is not correctly nested: " + input);
        }
    }
    public static bool isNested(in string input)
    {
        string filteredInput = extractBrackets(input);
        return filteredInput.Length >= 2;
    }


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

    private static string extractBrackets(in string input)
    {
        // Remove any character that is not a bracket or an apostrophe
        string result = String.Concat((input.Where((c) => "()[]{}'".Contains(c))));
        // Remove brackets enclosed in apostrophes
        result = System.Text.RegularExpressions.Regex.Replace(result, BRACKET_REGEX, "");

        return result;
    }
}
