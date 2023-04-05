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
        // TODO
        throw new NotImplementedException();
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