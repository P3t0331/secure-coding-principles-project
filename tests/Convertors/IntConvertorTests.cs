namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Enums;

[TestClass]

public class IntConvertorTest
{

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "1952805748";
        var convertor = new Convertor();

        string result = convertor.ConvertToBits(InputConvertor.ConvertInt(uint.Parse(input)));
        Assert.AreEqual("01110100011001010111001101110100", result);
    }

    [TestMethod]
    [Ignore]
    public void ConvertToByteArray()
    {

    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "1952805748";
        var convertor = new Convertor();

        string result = convertor.ConvertToBytes(InputConvertor.ConvertInt(uint.Parse(input)));
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHexBig()
    {
        string input = "1234567890";
        var convertor = new Convertor();

        string result = convertor.ConvertToHex(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));
        Assert.AreEqual("499602d2", result);
    }

    [TestMethod]
    public void ConvertToHexLittle()
    {
        string input = "1234567890";
        var convertor = new Convertor();

        string result = convertor.ConvertToHex(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Little));
        Assert.AreEqual("d2029649", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "1234567890";
        var convertor = new Convertor();

        string result = convertor.ConvertToInt(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));
        Assert.AreEqual("1234567890", result);
    }
}