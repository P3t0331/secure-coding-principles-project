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
        throw new NotImplementedException();
    }

    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big)
    {
        throw new NotImplementedException();
    }
}