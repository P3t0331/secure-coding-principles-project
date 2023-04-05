namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Enums;

[TestClass]

public class IntConvertorTest
{

    [TestMethod]
    [Ignore]
    public void ConvertToBits()
    {
        string input = "1952805748";
        var convertor = new Convertor(InputConvertor.ConvertInt(uint.Parse(input)));

        string result = convertor.ConvertToBits();
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
        var convertor = new Convertor(InputConvertor.ConvertInt(uint.Parse(input)));

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHexBig()
    {
        string input = "1234567890";
        var convertor = new Convertor(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));

        string result = convertor.ConvertToHex();
        Assert.AreEqual("499602D2", result);
    }

    [TestMethod]
    public void ConvertToHexLLittle()
    {
        string input = "1234567890";
        var convertor = new Convertor(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Little));

        string result = convertor.ConvertToHex();
        Assert.AreEqual("D2029649", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "1234567890";
        var convertor = new Convertor(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));

        string result = convertor.ConvertToInt();
        Assert.AreEqual("1234567890", result);
    }
}