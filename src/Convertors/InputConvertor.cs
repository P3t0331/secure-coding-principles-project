using Panbyte.Enums;
using System.Text;
using Panbyte.Validators;
namespace Panbyte.Convertors;

public static class InputConvertor
{
    public static byte[] ConvertBytes(string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static byte[] ConvertInt(uint input, Endianity inputEndianity = Endianity.Big)
    {
        byte[] result;
        result = BitConverter.GetBytes(input);
        if (inputEndianity == Endianity.Big)
        {
            Array.Reverse(result);
        }
        return result;
    }

    public static byte[] ConvertHex(string input)
    {
        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        return Enumerable.Range(0, input.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
                         .ToArray();
    }

    public static byte[] ConvertBits(string input, PaddingOrientation paddingOrientation = PaddingOrientation.Left)
    {
        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        input = PadBits(input, paddingOrientation);

        return Enumerable.Range(0, input.Length)
                         .Where(x => x % 8 == 0)
                         .Select(x => Convert.ToByte(input.Substring(x, 8), 2))
                         .ToArray();
    }

    public static byte[] ConvertArray(string input, Structs.ArrayOptions options)
    {
        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c) && !"{}[]()".Contains(c)));
        string[] inputList = input.Split(",");

        List<byte[]> result = new List<byte[]>();
        foreach (string element in inputList)
        {
            if (element.StartsWith("\\x") || element.StartsWith("0x"))
            {
                result.Add(ConvertHex(element.Substring(2)));
            }
            else if (element.StartsWith("0b"))
            {
                result.Add(ConvertBits(element.Substring(2)));
            }
            else
            {
                InputValidator.CheckIfUint(element);
                byte[] conversionResult = ConvertInt(uint.Parse(element)).Where((e) => e != 0).ToArray();
                if (conversionResult.Length == 0)
                {
                    conversionResult = new byte[] { 0x00 };
                }
                result.Add(conversionResult);
            }
        }

        return result.SelectMany(b => b).ToArray();
    }

    private static string PadBits(string input, PaddingOrientation paddingOrientation)
    {
        int totalChars = 8 * ((input.Length + 7) / 8);

        if (paddingOrientation == PaddingOrientation.Left)
        {
            return input.PadLeft(totalChars, '0');
        }
        else
        {
            return input.PadRight(totalChars, '0');
        }
    }

}