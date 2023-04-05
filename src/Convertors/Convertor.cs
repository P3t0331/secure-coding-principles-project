using System.Text;

namespace Panbyte.Convertors;

public class Convertor
{

    private byte[] input;

    public Convertor(byte[] input)
    {
        this.input = input;
    }

    public string ConvertToBits()
    {
        // TODO
        throw new NotImplementedException();
    }

    public string ConvertToByteArray()
    {
        // TODO
        throw new NotImplementedException();
    }

    public string ConvertToBytes()
    {
        return Encoding.UTF8.GetString(input);
    }

    public string ConvertToHex()
    {
        return BitConverter.ToString(input).Replace("-", string.Empty);
    }

    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big)
    {
        if (endianity == Enums.Endianity.Big)
        {
            Array.Reverse(input);
        }
        return BitConverter.ToUInt32(input, 0).ToString();
    }
}