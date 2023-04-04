namespace panbyte.Validators;

static class InputValidator
{
    public static bool CheckIfUint(string input)
    {
        try
        {
            uint.Parse(input);
            return true;

        } catch(OverflowException)
        {
            Console.WriteLine("This number is not uint! " + input);
            return false;
        } catch (FormatException)
        {
            Console.WriteLine("This input is not a number!: " + input);
            return false;
        } catch(ArgumentNullException)
        {
            Console.WriteLine("An unknown error has occured with this input: " + input);
            return false;
        }
    }

    public static bool CheckIfHex(string input)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(input, @"\A\b[0-9a-fA-F ]+\b\Z") || input.Length % 2 == 1) 
        {
            Console.WriteLine("Input is not in a valid hex format: " + input);
            return false;
        }
        return true;
    }
}
