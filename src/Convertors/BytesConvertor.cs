using System.Text;

namespace Panbyte.Convertors;

public class BytesConvertor : IConvertor
{

    private byte[] input;

    public BytesConvertor(string input)
    {
        this.input = Encoding.UTF8.GetBytes(input);
    }

    public string ConvertToBits()
    {
        throw new NotImplementedException();
    }

    public string ConvertToByteArray()
    {
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