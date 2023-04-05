namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]

public class HexConvertorTest
{

    [TestMethod]
    [Ignore]
    public void ConvertToBits()
    {
        string input = "74657374";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToBits();
        Assert.AreEqual("1110100011001010111001101110100", result);
    }

    [TestMethod]
    [Ignore]
    public void ConvertToByteArray()
    {

    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "74657374";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToBytesWhiteSpace()
    {
        string input = "74 65 73 74";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "74657374";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToHex();
        Assert.AreEqual("74657374", result);
    }

    [TestMethod]
    public void ConvertToIntBig()
    {
        string input = "499602d2";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToInt(Enums.Endianity.Big);
        Assert.AreEqual("1234567890", result);
    }

    [TestMethod]
    public void ConvertToIntLittle()
    {
        string input = "d2029649";
        var convertor = new Convertor(InputConvertor.ConvertHex(input));

        string result = convertor.ConvertToInt(Enums.Endianity.Little);
        Assert.AreEqual("1234567890", result);
    }
}