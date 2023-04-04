namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]
public class BitsConvertorTest
{

    [TestMethod]
    public void ConvertFromBitsLeft()
    {
        string input = "100111101001011";
        var convertor = new Convertor();

        string result = convertor.ConvertToBytes(InputConvertor.ConvertBits(input, Enums.PaddingOrientation.Left));
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public void ConvertFromBitsRight()
    {
        string input = "100111101001011";
        var convertor = new Convertor();

        string result = convertor.ConvertToHex(InputConvertor.ConvertBits(input, Enums.PaddingOrientation.Right));
        Assert.AreEqual("9e96", result);
    }

    [TestMethod]
    [Ignore]
    public void ConvertToByteArray()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "100 1111 0100 1011";
        var convertor = new Convertor();

        string result = convertor.ConvertToBytes(InputConvertor.ConvertBits(input));
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    [Ignore]
    public void ConvertToBytesWhiteSpace()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [Ignore]
    public void ConvertToHex()
    {
        Assert.Inconclusive();
    }

    [TestMethod]
    [Ignore]
    public void ConvertToInt()
    {
        Assert.Inconclusive();
    }
}