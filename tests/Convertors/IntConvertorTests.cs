using Panbyte.CLI;
using Panbyte.Structs;

namespace Panbyte.Tests.Convertors;
using Panbyte.Convertors;
using Panbyte.Enums;
using Panbyte.Utils;

[TestClass]

public class IntConvertorTest
{

    private readonly Structs.ArrayOptions defaultOptions = new Structs.ArrayOptions();

    [TestMethod]
    public void ConvertToBits()
    {
        string input = "1952805748";
        string result = Convertor.ConvertToBits(InputConvertor.ConvertInt(uint.Parse(input)));
        Assert.AreEqual("01110100011001010111001101110100", result);
    }

    [TestMethod]
    public void ConvertToByteArray()
    {
        string input = "16909060";
        string result = ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(InputConvertor.ConvertInt(uint.Parse(input)), defaultOptions), defaultOptions);
        Assert.AreEqual("{0x1, 0x2, 0x3, 0x4}", result);
    }

    [TestMethod]
    public void ConvertToBytes()
    {
        string input = "1952805748";
        string result = Convertor.ConvertToBytes(InputConvertor.ConvertInt(uint.Parse(input)));
        Assert.AreEqual("test", result);
    }

    [TestMethod]
    public void ConvertToHexBig()
    {
        string input = "1234567890";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));
        Assert.AreEqual("499602d2", result);
    }

    [TestMethod]
    public void ConvertToHexLittle()
    {
        string input = "1234567890";
        string result = Convertor.ConvertToHex(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Little));
        Assert.AreEqual("d2029649", result);
    }

    [TestMethod]
    public void ConvertToInt()
    {
        string input = "1234567890";
        string result = Convertor.ConvertToInt(InputConvertor.ConvertInt(uint.Parse(input), Endianity.Big));
        Assert.AreEqual("1234567890", result);
    }
    
    [TestMethod]
    public void ConvertIntToIntWithFaultyDelimiter()
    {
        Arguments arguments = new Arguments(Panbyte.Enums.Format.Int, new(), Panbyte.Enums.Format.Int, new(), null,
            null, "x");

        string input = "1\n2x12";
        
        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        InputProcessor inputProcessor = new InputProcessor(arguments);
        
        Assert.ThrowsException<FormatException>(()=>inputProcessor.ProcessInput());
    }
}