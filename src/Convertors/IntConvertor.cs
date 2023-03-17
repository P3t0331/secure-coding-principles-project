namespace Panbyte.Convertors;

public class IntConvertor : IConvertor
{

    private int input;

    public IntConvertor(string input, Enums.Endianity inputEndianity = Enums.Endianity.Big)
    {
        this.input = int.Parse(input);
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
        throw new NotImplementedException();
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