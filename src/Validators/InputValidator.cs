namespace Panbyte.Validators;
using System.Numerics;
using System.Text.RegularExpressions;

public static partial class InputValidator
{
    [GeneratedRegex(@"\A\b[0-9a-fA-F]+\b\Z")]
    private static partial Regex HEX_REGEX();

    public static void CheckIfUint(in string input)
    {
        try
        {
            if (BigInteger.Parse(input) < 0)
            {
                throw new OverflowException();
            }
        }
        catch (OverflowException)
        {
            throw new FormatException("This number is not uint!: " + input);
        }
        catch (FormatException)
        {
            throw new FormatException("This input is not a number!: " + input);
        }
        catch (ArgumentNullException)
        {
            throw new FormatException("An unknown error has occured with this input: " + input);
        }
    }

    public static void CheckIfHex(in string input)
    {
        string noWhitespace = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        bool isHex = HEX_REGEX().IsMatch(noWhitespace);
        bool isEven = noWhitespace.Length % 2 == 0;

        if (!isHex || !isEven)
        {
            throw new FormatException("Input is not in a valid hex format: " + noWhitespace);
        }
    }

    public static void CheckIfBits(in string input)
    {
        string noWhitespace = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        bool isBits = noWhitespace.All(c => c == '0' || c == '1');

        if (!isBits)
        {
            throw new FormatException("Input is not in a valid bits format: " + noWhitespace);
        }
    }
}
