namespace Panbyte.Validators;


public static class InputValidator
{
    private const string HEX_REGEX = @"\A\b[0-9a-fA-F]+\b\Z";

    public static void CheckIfUint(in string input)
    {
        try
        {
            uint.Parse(input);
        }
        catch (OverflowException)
        {
            throw new FormatException("This number is not uint! " + input);
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
        bool isHex = System.Text.RegularExpressions.Regex.IsMatch(noWhitespace, HEX_REGEX);
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
