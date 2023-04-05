using Panbyte.Enums;
using System.Text;
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
}