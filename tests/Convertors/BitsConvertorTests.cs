namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Utils;

[TestClass]
public class BitsConvertorTest
{
    private Structs.ArrayOptions defaultOptions = new Structs.ArrayOptions();

    [TestMethod]
    public void ConvertFromBitsLeft()
    {
        string input = "100111101001011";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertBits(input, Enums.PaddingOrientation.Left));
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public void ConvertFromBitsRight()
    {
        string input = "100111101001011";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertBits(input, Enums.PaddingOrientation.Right));
        Assert.AreEqual("9e96", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {
        string input = "00000001000000100000001100000100";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertBits(input), defaultOptions), defaultOptions);
        Assert.AreEqual("{0x1, 0x2, 0x3, 0x4}", result);
    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "100 1111 0100 1011";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertBits(input));
        Assert.AreEqual("OK", result);
    }

    [TestMethod]
    public void ConvertToHex()
    {
        string input = "100111101001011";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertBits(input));
        Assert.AreEqual("4f4b", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "100111101001011";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertBits(input));
        Assert.AreEqual("20299", result);
    }
}