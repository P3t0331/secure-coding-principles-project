namespace Panbyte.Validators;
using Panbyte.Utils;


public static class ArrayValidator
{
    private const string CHAR_REGEX = @"^'[ -~]{1}'$";
    private const string XHEX_REGEX = @"^'\\x[0-9a-fA-F]{2}'$";
    private const string HEX_REGEX = @"^0x[0-9a-fA-F]{2}$";
    private const string BITS_REGEX = @"^0b[01]{1,8}$";

    public static void CheckValidPosition(in string input)
    {
        string inputNoWhiteSpace = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        string filteredInput = ByteArrayUtils.removeBracketsInApostrophes(inputNoWhiteSpace);

        bool lastBracketIsOpen = false;
        bool isCommaPresent = false;
        bool characterBetweenBrackets = false;

        for (int i = 0; i < filteredInput.Length; i++)
        {
            switch (filteredInput[i])
            {
                case '{':
                case '[':
                case '(':
                    if ((characterBetweenBrackets || !isCommaPresent && i != 0) && !(!characterBetweenBrackets && lastBracketIsOpen))
                    {
                        throw new FormatException("Invalid array format");
                    }

                    isCommaPresent = false;
                    characterBetweenBrackets = false;
                    lastBracketIsOpen = true;
                    break;
                case '}':
                case ']':
                case ')':
                    if (!lastBracketIsOpen && !isCommaPresent && characterBetweenBrackets)
                    {
                        throw new FormatException("Invalid array format");
                    }

                    isCommaPresent = false;
                    characterBetweenBrackets = false;
                    lastBracketIsOpen = false;
                    break;
                case ',':
                    isCommaPresent = true;
                    characterBetweenBrackets = false;
                    break;
                default:
                    if (!isCommaPresent && !lastBracketIsOpen)
                    {
                        throw new FormatException("Invalid array format");
                    }
                    characterBetweenBrackets = true;
                    break;
            }
        }
    }
    public static void CheckCorrectNesting(in string input)
    {
        string filteredInput = ByteArrayUtils.extractBrackets(input);
        filteredInput = ByteArrayUtils.removeBracketsInApostrophes(filteredInput);

        int curlyOpenCount = 0;
        int squareOpenCount = 0;
        int roundOpenCount = 0;

        for (int i = 0; i < filteredInput.Length; i++)
        {
            switch (filteredInput[i])
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
                    throw new Exception("Unexpected character: " + filteredInput[i]);
            }

            if (curlyOpenCount == 0 && squareOpenCount == 0 && roundOpenCount == 0 && i < filteredInput.Length - 1)
            {
                throw new FormatException("Input is not correctly nested: " + input);
            }
        }

        if (curlyOpenCount != 0 || squareOpenCount != 0 || roundOpenCount != 0)
        {
            throw new FormatException("Input is not correctly nested: " + input);
        }
    }
    public static bool isNested(in string input)
    {
        string filteredInput = ByteArrayUtils.extractBrackets(input);
        filteredInput = ByteArrayUtils.removeBracketsInApostrophes(filteredInput);
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
        return System.Text.RegularExpressions.Regex.IsMatch(input, CHAR_REGEX);
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
