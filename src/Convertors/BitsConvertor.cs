namespace Panbyte.Convertors;
using Panbyte.Enums;
using System.Collections;

public class BitsConvertor : IConvertor
{

    public BitsConvertor(string input, PaddingOrientation paddingOrientation = Enums.PaddingOrientation.Left) { }

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