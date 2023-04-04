namespace Panbyte.Convertors;

public class ConvertorFactory
{
    public static IConvertor CreateConvertor(Structs.Arguments cliArgs)
    {
        switch (cliArgs.inputFormat)
        {
            case Enums.Format.Bytes:
                return new BytesConvertor();
            case Enums.Format.Hex:
                return new HexConvertor();
            case Enums.Format.Int:
                return new IntConvertor(cliArgs.inputOptions);
            case Enums.Format.Bits:
                return new BitsConvertor(cliArgs.inputOptions);
            case Enums.Format.Array:
                return new ByteArrayConvertor();
            default:
                throw new ArgumentException("Argument not recognized (yet): " + cliArgs.inputFormat);
        }
    }
}