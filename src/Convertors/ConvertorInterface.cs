namespace Panbyte.Convertors;

interface IConvertor
{
    public string ConvertToBytes();
    public string ConvertToHex();
    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big);
    public string ConvertToBits();
    public string ConvertToByteArray();
}