namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Utils;

[TestClass]
public class ByteArrayConvertorTest
{
    private readonly Structs.ArrayOptions squareBracketOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Square, format: Enums.ArrayFormat.Hex);
    private readonly Structs.ArrayOptions roundBracketOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Round, format: Enums.ArrayFormat.Hex);
    private readonly Structs.ArrayOptions toDecimalFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Decimal);
    private readonly Structs.ArrayOptions toBinaryFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Binary);
    private readonly Structs.ArrayOptions toCharFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Char);

    [TestMethod]
    public void ConvertToHex()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertArray(input));
        Assert.AreEqual("01020304", result);
    }


    [TestMethod]
    public void ConvertToBits()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        string result = Convertor.ConvertToBits(InputConvertor.ConvertArray(input));
        Assert.AreEqual("00000001000000100000001100000100", result);
    }


    [TestMethod]
    public void ConvertToBytes()
    {
        string input = @"{0x61, 98, 0b1100011, '\x64'}";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertArray(input));
        Assert.AreEqual("abcd", result);
    }


    [TestMethod]
    public void ConvertToInt()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertArray(input));
        Assert.AreEqual("16909060", result);
    }

    [TestMethod]
    public void ConvertToArraySquareBrackets()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), squareBracketOptions), squareBracketOptions);
        Assert.AreEqual("[0x1, 0x2, 0x3, 0x4]", result);
    }

    [TestMethod]
    public void ConvertToArrayRoundBrackets()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), roundBracketOptions), roundBracketOptions);
        Assert.AreEqual("(0x1, 0x2, 0x3, 0x4)", result);
    }

    [TestMethod]
    public void ConvertToArrayDecimalOutput()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toDecimalFormatOptions), toDecimalFormatOptions);
        Assert.AreEqual("{1, 2, 3, 4}", result);
    }

    [TestMethod]
    public void ConvertToArrayBinaryOutput()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toBinaryFormatOptions), toBinaryFormatOptions);
        Assert.AreEqual("{0b1, 0b10, 0b11, 0b100}", result);
    }

    [TestMethod]
    public void ConvertToArrayCharOutput()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toCharFormatOptions), toCharFormatOptions);
        Assert.AreEqual(@"{'\x01', '\x02', '\x03', '\x04'}", result);
    }

}