using System.Text;
using System.Numerics;

namespace Panbyte.Convertors;

public static class Convertor
{
    public static string ConvertToBits(byte[] input)
    {
        string bitString = string.Concat(input.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        return bitString;
    }

    public static string ConvertToByteArray(byte[] input, Structs.ArrayOptions options)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            byte[] inputArray = new byte[1];
            inputArray[0] = input[i];
            switch (options.format)
            {
                case Enums.ArrayFormat.Hex:
                    result.Append("0x");
                    var converted = ConvertToHex(inputArray);
                    result.Append(converted.TrimStart('0'));
                    break;
                case Enums.ArrayFormat.Decimal:
                    result.Append(ConvertToInt(inputArray, Enums.Endianity.Little));
                    break;
                case Enums.ArrayFormat.Char:
                    if (inputArray[0] >= 32 && inputArray[0] <= 126)
                    {
                        result.Append(ConvertToBytes(inputArray));  
                    }
                    else
                    {
                        result.Append("'\\x");
                        result.Append(ConvertToHex(inputArray).AsSpan(0, 2));
                        result.Append('\'');
                    }
                    break;
                case Enums.ArrayFormat.Binary:
                    result.Append("0b");
                    result.Append(ConvertToBits(inputArray).TrimStart('0'));
                    break;
                default:
                    break;
            }

            if (i != input.Length - 1)
            {
                result.Append(", ");
            }
        }

        return result.ToString();

    }
    public static string ConvertToBytes(byte[] input)
    {
        return Encoding.UTF8.GetString(input);
    }

    public static string ConvertToHex(byte[] input)
    {
        return BitConverter.ToString(input).Replace("-", string.Empty).ToLower();
    }

    public static string ConvertToInt(byte[] input, Enums.Endianity endianity = Enums.Endianity.Big)
    {
        if (endianity == Enums.Endianity.Big)
        {
            Array.Reverse(input);
        }
        return new BigInteger(input).ToString();
    }
}