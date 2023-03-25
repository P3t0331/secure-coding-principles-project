namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]

public class BytesConvertorTest
{

    [TestMethod]
    [Ignore]
    public void ConvertToBits()
    {
        string input = "OK";
        var convertor = new BytesConvertor(input);

        string result = convertor.ConvertToBits();
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
        var convertor = new BytesConvertor(input);

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "test";
        var convertor = new BytesConvertor(input);

        string result = convertor.ConvertToHex();
        Assert.AreEqual("74657374", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "test";
        var convertor = new BytesConvertor(input);

        string result = convertor.ConvertToInt();
        Assert.AreEqual("1952805748", result);
    }
}