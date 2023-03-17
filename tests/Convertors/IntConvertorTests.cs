namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]

public class IntConvertorTest
{

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "1952805748";
        var convertor = new IntConvertor(input);

        string result = convertor.ConvertToBits();
        Assert.AreEqual("01110100011001010111001101110100", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {

    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "1952805748";
        var convertor = new IntConvertor(input);

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHexBig()
    {
        string input = "1234567890";
        var convertor = new IntConvertor(input, Enums.Endianity.Big);

        string result = convertor.ConvertToHex();
        Assert.AreEqual("499602d2", result);
    }

    [TestMethod]
    public void ConvertToHexLLittle()
    {
        string input = "1234567890";
        var convertor = new IntConvertor(input, Enums.Endianity.Little);

        string result = convertor.ConvertToHex();
        Assert.AreEqual("d2029649", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "1234567890";
        var convertor = new IntConvertor(input);

        string result = convertor.ConvertToInt();
        Assert.AreEqual("1234567890", result);
    }
}