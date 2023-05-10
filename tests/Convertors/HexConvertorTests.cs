namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Exceptions;
using Panbyte.Utils;

[TestClass]

public class HexConvertorTest
{

    private readonly Structs.ArrayOptions defaultOptions = new Structs.ArrayOptions();

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "74657374";
        string result = Convertor.ConvertToBits(InputConvertor.ConvertHex(input));
        Assert.AreEqual("01110100011001010111001101110100", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {
        string input = "01020304";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertHex(input), defaultOptions), defaultOptions);
        Assert.AreEqual("{0x1, 0x2, 0x3, 0x4}", result);
    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "74657374";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertHex(input));
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToBytesWhiteSpace()
    {
        string input = "74 65 73 74";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertHex(input));
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "74657374";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertHex(input));
        Assert.AreEqual("74657374", result);
    }

    [TestMethod]
    public void ConvertToIntBig()
    {
        string input = "499602d2";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertHex(input), Enums.Endianity.Big);
        Assert.AreEqual("1234567890", result);
    }

    [TestMethod]
    public void ConvertToIntLittle()
    {
        string input = "d2029649";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertHex(input), Enums.Endianity.Little);
        Assert.AreEqual("1234567890", result);
    }

    [TestMethod]
    public void ConvertToIntSignedValue()
    {
        string input = "80";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertHex(input));
        Assert.AreEqual("128", result);
    }

    [TestMethod]
    public void ConvertToIntEdge()
    {
        string input = "FFFFFFFF";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertHex(input));
        Assert.AreEqual("4294967295", result);
    }
}