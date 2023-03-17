namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;

[TestClass]

public class BitsConvertorTest
{

    [TestMethod]
    public void ConvertToBitsLeft()
    {
        string input = "100 1111 0100 1011";
        var convertor = new BitsConvertor(input, Enums.PaddingOrientation.Left);

        string result = convertor.ConvertToBits();
        Assert.AreEqual("0100111101001011", result);
    }

    public void ConvertToBitsRight()
    {
        string input = "100 1111 0100 1011";
        var convertor = new BitsConvertor(input, Enums.PaddingOrientation.Right);

        string result = convertor.ConvertToBits();
        Assert.AreEqual("1001111010010110", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {

    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "100111101001011";
        var convertor = new BitsConvertor(input, Enums.PaddingOrientation.Left);

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public void ConvertToBytesWhiteSpace()
    {
        string input = "100 1111 0100 1011";
        var convertor = new BitsConvertor(input, Enums.PaddingOrientation.Left);

        string result = convertor.ConvertToBytes();
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "100111101001011";
        var convertor = new BitsConvertor(input, Enums.PaddingOrientation.Right);

        string result = convertor.ConvertToHex();
        Assert.AreEqual("9e96", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "100111101001011";
        var convertor = new BitsConvertor(input);

        string result = convertor.ConvertToInt();
        Assert.AreEqual("20299", result);
    }
}