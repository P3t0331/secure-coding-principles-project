namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Utils;

[TestClass]
public class ByteArrayConvertorTest
{
    private Structs.ArrayOptions defaultOptions = new Structs.ArrayOptions();
    private Structs.ArrayOptions squareBracketOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Square, format: Enums.ArrayFormat.Hex);
    private Structs.ArrayOptions roundBracketOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Round, format: Enums.ArrayFormat.Hex);
    private Structs.ArrayOptions toDecimalFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Decimal);
    private Structs.ArrayOptions toBinaryFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Binary);
    private Structs.ArrayOptions toCharFormatOptions = new Structs.ArrayOptions(bracket: Enums.Bracket.Curly, format: Enums.ArrayFormat.Char);

    [TestMethod]
    public void ConvertToHex()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        var convertor = new Convertor();

        string result = convertor.ConvertToHex(InputConvertor.ConvertArray(input, defaultOptions));
        Assert.AreEqual("01020304", result);
    }


    [TestMethod]
    public void ConvertToBits()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        var convertor = new Convertor();

        string result = convertor.ConvertToBits(InputConvertor.ConvertArray(input, defaultOptions));
        Assert.AreEqual("00000001000000100000001100000100", result);
    }


    [TestMethod]
    public void ConvertToBytes()
    {
        string input = @"{0x61, 98, 0b1100011, '\x64'}";
        var convertor = new Convertor();

        string result = convertor.ConvertToBytes(InputConvertor.ConvertArray(input, defaultOptions));
        Assert.AreEqual("abcd", result);
    }


    [TestMethod]
    public void ConvertToInt()
    {
        string input = @"{0x01, 2, 0b11, '\x04'}";
        var convertor = new Convertor();

        string result = convertor.ConvertToInt(InputConvertor.ConvertArray(input, defaultOptions));
        Assert.AreEqual("16909060", result);
    }

    [TestMethod]
    public void ConvertToArraySquareBrackets()
    {
        string input = "01020304";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), squareBracketOptions), squareBracketOptions);
        Assert.AreEqual("[0x1, 0x2, 0x3, 0x4]", result);
    }

    [TestMethod]
    public void ConvertToArrayRoundBrackets()
    {
        string input = "01020304";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), roundBracketOptions), roundBracketOptions);
        Assert.AreEqual("(0x1, 0x2, 0x3, 0x4)", result);
    }

    [TestMethod]
    public void ConvertToArrayDecimalOutput()
    {
        string input = "01020304";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toDecimalFormatOptions), toDecimalFormatOptions);
        Assert.AreEqual("{1, 2, 3, 4}", result);
    }

    [TestMethod]
    public void ConvertToArrayBinaryOutput()
    {
        string input = "01020304";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toBinaryFormatOptions), toBinaryFormatOptions);
        Assert.AreEqual("{0b1, 0b10, 0b11, 0b100}", result);
    }

    [TestMethod]
    public void ConvertToArrayCharOutput()
    {
        string input = "01020304";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), toCharFormatOptions), toCharFormatOptions);
        Assert.AreEqual(@"{'\x01', '\x02', '\x03', '\x04'}", result);
    }



}