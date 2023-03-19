namespace Panbyte.Convertors;
public class HexConvertor : IConvertor
{
    private string input;

    public HexConvertor(string input)
    {
        this.input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
    }

    public string ConvertToHex()
    {
        return input;
    }

    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big)
    {
        if (endianity == Enums.Endianity.Little)
        {
            UInt32 number = Convert.ToUInt32(input, 16);
            byte[] bytes = BitConverter.GetBytes(number);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            input = retval;
        }
        var value = UInt32.Parse(input, System.Globalization.NumberStyles.HexNumber);
        return value.ToString();
    }

    public string ConvertToBits()
    {
        return "";
    }

    public string ConvertToBytes()
    {
        return System.Text.Encoding.UTF8.GetString(Convert.FromHexString(input));
    }

    public string ConvertToByteArray()
    {
        throw new NotImplementedException();
    }
}