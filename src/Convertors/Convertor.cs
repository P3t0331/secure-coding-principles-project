using System.Text;

namespace Panbyte.Convertors;

public class Convertor
{
    public string ConvertToBits(byte[] input)
    {
        string bitString = string.Concat(input.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        return bitString;
    }

    public string ConvertToByteArray(byte[] input, Structs.ArrayOptions options)
    {
        string result = "";

        for (int i = 0; i < input.Length; i++)
        {
            byte[] inputArray = new byte[1];
            inputArray[0] = input[i];
            switch (options.format)
            {
                case Enums.ArrayFormat.Hex:
                    result += "0x" + ConvertToHex(inputArray).Replace("0", "");
                    break;
                case Enums.ArrayFormat.Decimal:
                    // Array needs to be at least 4 bytes long for Int conversion
                    inputArray = new byte[4];
                    inputArray[0] = input[i];
                    result += ConvertToInt(inputArray, Enums.Endianity.Little);
                    break;
                case Enums.ArrayFormat.Char:
                    if (inputArray[0] >= 32 && inputArray[0] <= 126)
                    {
                        result += ConvertToBytes(inputArray);
                    }
                    else
                    {
                        result += "'\\x" + ConvertToHex(inputArray).Substring(0, 2) + "'";
                    }
                    break;
                case Enums.ArrayFormat.Binary:
                    result += "0b" + ConvertToBits(inputArray).TrimStart('0');
                    break;
                default:
                    break;
            }

            if (i != input.Length - 1)
            {
                result += ", ";
            }
        }

        return result;

    }
    public string ConvertToBytes(byte[] input)
    {
        return Encoding.UTF8.GetString(input);
    }

    public string ConvertToHex(byte[] input)
    {
        return BitConverter.ToString(input).Replace("-", string.Empty).ToLower();
    }

    public string ConvertToInt(byte[] input, Enums.Endianity endianity = Enums.Endianity.Big)
    {
        if (endianity == Enums.Endianity.Big)
        {
            Array.Reverse(input);
        }
        return BitConverter.ToUInt32(input, 0).ToString();
    }
}