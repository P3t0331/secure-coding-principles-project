namespace Panbyte.Convertors;

public class IntConvertor : IConvertor
{

    private UInt32 input;
    private Enums.Endianity inputEndianity;

    public IntConvertor(string input, Enums.Endianity inputEndianity = Enums.Endianity.Big)
    {
        this.inputEndianity = inputEndianity;
        this.input = UInt32.Parse(input);
        if (inputEndianity == Enums.Endianity.Little)
        {
            byte[] bytes = BitConverter.GetBytes(this.input);
            Array.Reverse(bytes);
            this.input = BitConverter.ToUInt32(bytes, 0);
        }
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
        byte[] bytes = BitConverter.GetBytes(input);
        Array.Reverse(bytes);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }

    public string ConvertToHex()
    {
        return input.ToString("X4").ToLower();
    }

    public string ConvertToInt(Enums.Endianity endianity = Enums.Endianity.Big)
    {
        if (inputEndianity == endianity)
        {
            return input.ToString();
        }
        else
        {
            byte[] bytes = BitConverter.GetBytes(input);
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0).ToString();
        }
    }
}