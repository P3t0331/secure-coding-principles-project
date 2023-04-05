namespace Panbyte.Tests.CLI;
using Panbyte.CLI;
using Utils;

[TestClass]
public class ArgumentsParserTest
{
    [TestMethod]
    public void TestAllArgsShortForm()
    {
        string[] flags = new string[] { "-f", "int", "--from-options=little", "-t", "bits", "--to-options=left", "-i", "input.txt", "-o", "output.txt", "-d", "," };
        TestAllArgs(flags);
    }

    [TestMethod]
    public void TestAllArgsLongForm()
    {
        string[] flags = new string[] { "--from=int", "--from-options=little", "--to=bits", "--to-options=left", "--input=input.txt", "--output=output.txt", "--delimiter=," };
        TestAllArgs(flags);
    }

    [TestMethod]
    public void TestMultipleOptions()
    {
        string[] inputOptions = new string[] { "0x", "]" };
        string[] outputOptions = new string[] { "a", "}" };

        string[] args = new string[] {
            "-f",
            "hex",
            "-t",
            "bytes",
            "--from-options=" + inputOptions[0],
            "--from-options=" + inputOptions[1],
            "--to-options=" + outputOptions[0],
            "--to-options=" + outputOptions[1],
        };

        Structs.Arguments arguments = ArgumentsParser.Parse(args);

        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.inputOptions.ToArray(), inputOptions));
        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.outputOptions.ToArray(), outputOptions));
    }

    [TestMethod]
    public void TestNoArgs()
    {
        string[] args = new string[] { };
        Assert.ThrowsException<ArgumentNullException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestUndefinedArgument()
    {
        string[] args = new string[] { "-f", "int", "-t", "bits", "something" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestUndefinedArgumentAfterValue()
    {
        string[] args = new string[] { "-f", "int", "-t", "bits", "-d", ",", "asd" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestMissingValue()
    {
        string[] args = new string[] { "-f", "int", "-t", "bits", "-o" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestSameArgMultipleTimes()
    {
        string[] args = new string[] { "-f", "int", "-t", "bits", "-d", ".", "-d", "," };
        Assert.AreEqual(ArgumentsParser.Parse(args).delimiter, ",");
    }

    private void TestAllArgs(string[] args)
    {
        string inputEndianity = "little";
        string outputPaddingOrientation = "left";
        string inputPath = "input.txt";
        string outputPath = "output.txt";
        string delimiter = ",";
        string[] inputOptions = new string[] { inputEndianity };
        string[] outputOptions = new string[] { outputPaddingOrientation };

        Structs.Arguments arguments = ArgumentsParser.Parse(args);

        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.inputOptions.ToArray(), inputOptions));
        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.outputOptions.ToArray(), outputOptions));
        Assert.AreEqual(arguments.inputFormat, Enums.Format.Int);
        Assert.AreEqual(arguments.outputFormat, Enums.Format.Bits);
        Assert.AreEqual(arguments.inputPath, inputPath);
        Assert.AreEqual(arguments.outputPath, outputPath);
        Assert.AreEqual(arguments.delimiter, delimiter);
    }
}
