using Panbyte.Enums;
namespace Panbyte.Structs;

public readonly struct ArrayOptions
{
    public readonly Bracket bracket;
    public readonly ArrayFormat format;

    public ArrayOptions(Bracket? bracket, ArrayFormat? format)
    {
        this.bracket = bracket ?? Bracket.Curly;
        this.format = format ?? ArrayFormat.Hex;
    }
}