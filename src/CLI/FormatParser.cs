namespace Panbyte.CLI;

public class FormatParser
{
    public static Enums.Format Parse(string format)
    {
        switch (format)
        {
            case "bytes":
                return Enums.Format.Bytes;
            case "hex":
                return Enums.Format.Hex;
            case "int":
                return Enums.Format.Int;
            case "bits":
                return Enums.Format.Bits;
            case "array":
                return Enums.Format.Array;
            default:
                throw new ArgumentException("Unknown format: " + format);
        }
    }
}