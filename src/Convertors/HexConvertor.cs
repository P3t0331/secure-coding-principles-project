namespace Panbyte.Convertors;

public class HexConvertor : IConvertor
{
    private string input;

    public HexConvertor(string input)
    {
        this.input = input;
    }

    public string ConvertToHex()
    {
        return "";
    }

    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big)
    {
        return "";
    }

    public string ConvertToBits()
    {
        return "";
    }

    public string ConvertToBytes()
    {
        throw new NotImplementedException();
    }

    public string ConvertToByteArray()
    {
        throw new NotImplementedException();
    }
}