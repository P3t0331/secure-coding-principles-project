namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Utils;

[TestClass]

public class BytesConvertorTest
{

    private Structs.ArrayOptions defaultOptions = new Structs.ArrayOptions();

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "OK";
        var convertor = new Convertor();

        string result = convertor.ConvertToBits(InputConvertor.ConvertBytes(input));
        Assert.AreEqual("0100111101001011", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {
        string input = "abcd";
        var convertor = new Convertor();

        string result = ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(InputConvertor.ConvertBytes(input), defaultOptions), defaultOptions);
        Assert.AreEqual("{0x61, 0x62, 0x63, 0x64}", result);
    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "test";
        var convertor = new Convertor();

        string result = convertor.ConvertToBytes(InputConvertor.ConvertBytes(input));
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "test";
        var convertor = new Convertor();

        string result = convertor.ConvertToHex(InputConvertor.ConvertBytes(input));
        Assert.AreEqual("74657374", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "test";
        var convertor = new Convertor();

        string result = convertor.ConvertToInt(InputConvertor.ConvertBytes(input));
        Assert.AreEqual("1952805748", result);
    }
}