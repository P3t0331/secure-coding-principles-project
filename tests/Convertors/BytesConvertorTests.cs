namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]

public class BytesConvertorTest
{

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "OK";
        var convertor = new Convertor();

        string result = convertor.ConvertToBits(InputConvertor.ConvertBytes(input));
        Assert.AreEqual("0100111101001011", result);
    }

    [TestMethod]
    [Ignore]
    public void ConvertToByteArray()
    {

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