namespace Panbyte.Validators;

using System.Text.RegularExpressions;
using Panbyte.Utils;


public static partial class ArrayValidator
{
    [GeneratedRegex(@"^'[ -~]{1}'$")]
    private static partial Regex CHAR_REGEX();

    [GeneratedRegex(@"^'\\x[0-9a-fA-F]{2}'$")]
    private static partial Regex XHEX_REGEX();

    [GeneratedRegex(@"^0x[0-9a-fA-F]{2}$")]
    private static partial Regex HEX_REGEX();

    [GeneratedRegex(@"^0b[01]{1,8}$")]
    private static partial Regex BITS_REGEX();

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
                    throw new FormatException("Unexpected character: " + filteredInput[i]);
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
        return HEX_REGEX().IsMatch(input);
    }
    public static bool isCharHex(in string input)
    {
        return XHEX_REGEX().IsMatch(input);
    }
    public static bool isChar(in string input)
    {
        return CHAR_REGEX().IsMatch(input);
    }
    public static bool isDecimal(in string input)
    {
        return (int.TryParse(input, out int value) && value >= 0 && value <= 255);
    }

    public static bool isBits(in string input)
    {
        return BITS_REGEX().IsMatch(input);
    }
}
