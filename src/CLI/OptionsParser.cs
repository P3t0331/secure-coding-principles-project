using Panbyte.Enums;
using Panbyte.Structs;
namespace Panbyte.CLI;

public class OptionsParser
{
    public static Endianity ParseEndianity(List<string> options)
    {
        Endianity endianity = Endianity.Big;

        options.ForEach(option =>
        {
            switch (option)
            {
                case "big":
                    endianity = Endianity.Big;
                    break;
                case "little":
                    endianity = Endianity.Little;
                    break;
                default:
                    throw new ArgumentException("Argument not recognized: " + option);
            }
        });

        return endianity;
    }

    public static PaddingOrientation ParsePadding(List<string> options)
    {
        PaddingOrientation orientation = PaddingOrientation.Left;

        options.ForEach(option =>
        {
            switch (option)
            {
                case "left":
                    orientation = PaddingOrientation.Left;
                    break;
                case "right":
                    orientation = PaddingOrientation.Right;
                    break;
                default:
                    throw new ArgumentException("Argument not recognized: " + option);
            }
        });

        return orientation;
    }

    public static ArrayOptions ParseArrayOptions(List<string> options)
    {
        Bracket? bracket = null;
        ArrayFormat? format = null;

        options.ForEach(option =>
        {
            switch (option)
            {
                case "0x":
                    format = ArrayFormat.Hex;
                    break;
                case "0":
                    format = ArrayFormat.Decimal;
                    break;
                case "a":
                    format = ArrayFormat.Char;
                    break;
                case "0b":
                    format = ArrayFormat.Binary;
                    break;
                case "{":
                case "}":
                    bracket = Bracket.Curly;
                    break;
                case "[":
                case "]":
                    bracket = Bracket.Square;
                    break;
                case "(":
                case ")":
                    bracket = Bracket.Round;
                    break;
                default:
                    throw new ArgumentException("Argument not recognized: " + option);
            }
        });

        return new ArrayOptions(bracket, format);
    }
}