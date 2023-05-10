namespace Panbyte.Tests.Convertors;
using Panbyte.CLI;

[TestClass]
public class NestedByteArrayTest
{

    private static Structs.Arguments defaultArgs = new Structs.Arguments(
        inputFormat: Enums.Format.Array,
        inputOptions: new List<string> { },
        outputFormat: Enums.Format.Array,
        outputOptions: new List<string> { },
        inputPath: null,
        outputPath: null,
        delimiter: null
        );
    private InputProcessor defaultInputProcessor = new InputProcessor(cliArgs: defaultArgs);

    [TestMethod]
    public void ConvertArrayExample1()
    {

        string input = @"[[1, 2], [3, 4], [5, 6]]";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        defaultInputProcessor.ProcessInput();
        Assert.AreEqual("{{0x1, 0x2}, {0x3, 0x4}, {0x5, 0x6}}\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void ConvertArrayExample2()
    {
        Structs.Arguments cliArgs = new Structs.Arguments(
        inputFormat: Enums.Format.Array,
        inputOptions: new List<string> { },
        outputFormat: Enums.Format.Array,
        outputOptions: new List<string> { @"{", "0" },
        inputPath: null,
        outputPath: null,
        delimiter: null
        );
        InputProcessor inputProcessor = new InputProcessor(cliArgs: cliArgs);

        string input = @"[[1, 2], [3, 4], [5, 6]]";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        inputProcessor.ProcessInput();
        Assert.AreEqual("{{1, 2}, {3, 4}, {5, 6}}\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void ConvertArrayExample3()
    {
        Structs.Arguments cliArgs = new Structs.Arguments(
        inputFormat: Enums.Format.Array,
        inputOptions: new List<string> { },
        outputFormat: Enums.Format.Array,
        outputOptions: new List<string> { @"0", "[" },
        inputPath: null,
        outputPath: null,
        delimiter: null
        );
        InputProcessor inputProcessor = new InputProcessor(cliArgs: cliArgs);

        string input = @"{{0x01, (2), [3, 0b100, 0x05], '\x06'}}";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        inputProcessor.ProcessInput();
        Assert.AreEqual("[[1, [2], [3, 4, 5], 6]]\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void ConvertArrayExample4()
    {

        string input = @"()";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        defaultInputProcessor.ProcessInput();
        Assert.AreEqual("{}\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void ConvertArrayExample5()
    {
        Structs.Arguments cliArgs = new Structs.Arguments(
        inputFormat: Enums.Format.Array,
        inputOptions: new List<string> { },
        outputFormat: Enums.Format.Array,
        outputOptions: new List<string> { "[" },
        inputPath: null,
        outputPath: null,
        delimiter: null
        );
        InputProcessor inputProcessor = new InputProcessor(cliArgs: cliArgs);

        string input = @"([],{})";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        inputProcessor.ProcessInput();
        Assert.AreEqual("[[], []]\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void ConvertArrayDeep()
    {

        string input = @"{1, 2, [5, [6, [7], {8}, (9, 10)], [11], 12], {13}}";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        defaultInputProcessor.ProcessInput();
        Assert.AreEqual("{0x1, 0x2, {0x5, {0x6, {0x7}, {0x8}, {0x9, 0xa}}, {0xb}, 0xc}, {0xd}}\r\n".Trim(), stringWriter.ToString().Trim());
    }

    [TestMethod]
    public void HexToHexWithTrailingZeroes()
    {
        string input = @"{0xf0}";

        StringReader stringReader = new StringReader(input);
        Console.SetIn(stringReader);

        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        defaultInputProcessor.ProcessInput();
        Assert.AreEqual("{0xf0}", stringWriter.ToString().Trim());
    }
}