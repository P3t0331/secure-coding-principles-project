using System.Text;

namespace Panbyte.Convertors;

public class Convertor
{
    public string ConvertToBits(byte[] input)
    {
        // TODO
        throw new NotImplementedException();
    }

    public string ConvertToByteArray(byte[] input)
    {
        // TODO
        throw new NotImplementedException();
    }

    public string ConvertToBytes(byte[] input)
    {
        byte[] byteArrInput = ParseInput(input);
        return Encoding.UTF8.GetString(byteArrInput);
    }

    public string ConvertToHex(byte[] input)
    {
        byte[] byteArrInput = ParseInput(input);
        return BitConverter.ToString(byteArrInput).Replace("-", string.Empty);
    }

    public string ConvertToInt(byte[] input, Enums.Endianity endianity = Enums.Endianity.Big)
    {
        byte[] byteArrInput = ParseInput(input);
        if (endianity == Enums.Endianity.Big)
        {
            Array.Reverse(byteArrInput);
        }
        return BitConverter.ToUInt32(byteArrInput, 0).ToString();
    }

    private byte[] ParseInput(byte[] input)
    {
        return Encoding.UTF8.GetBytes(input);
    }
}