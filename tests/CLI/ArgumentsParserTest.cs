namespace Panbyte.Tests.CLI;
using Panbyte.CLI;
using Utils;

[TestClass]
public class ArgumentsParserTest
{
    [TestMethod]
    public void TestAllArgsShortForm()
    {
        string[] flags = new string[] { "-f", "-t", "-i", "-o", "-d", "-h" };
        TestAllArgs(flags);
    }

    [TestMethod]
    public void TestAllArgsLongForm()
    {
        string[] flags = new string[] { "--from", "--to", "--input", "--output", "--delimiter", "--help" };
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
        Structs.Arguments arguments = ArgumentsParser.Parse(args);

        Assert.IsFalse(arguments.help);
        Assert.IsNull(arguments.delimiter);
        Assert.IsNull(arguments.inputFormat);
        Assert.IsFalse(arguments.inputOptions.Any());
        Assert.IsNull(arguments.inputPath);
        Assert.IsNull(arguments.outputFormat);
        Assert.IsFalse(arguments.outputOptions.Any());
        Assert.IsNull(arguments.outputPath);
    }

    [TestMethod]
    public void TestUndefinedArgument()
    {
        string[] args = new string[] { "something" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestUndefinedArgumentAfterValue()
    {
        string[] args = new string[] { "-d", ",", "asd" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestMissingValue()
    {
        string[] args = new string[] { "-o" };
        Assert.ThrowsException<ArgumentException>(() => ArgumentsParser.Parse(args));
    }

    [TestMethod]
    public void TestSameArgMultipleTimes()
    {
        string[] args = new string[] { "-d", ".", "-d", "," };
        Assert.AreEqual(ArgumentsParser.Parse(args).delimiter, ",");
    }

    private void TestAllArgs(string[] flags)
    {
        string inputFormat = "int";
        string inputEndianity = "big";
        string outputFormat = "bits";
        string outputPaddingOrientation = "left";
        string inputPath = "input.txt";
        string outputPath = "output.txt";
        string delimiter = ",";
        string[] inputOptions = new string[] { inputEndianity };
        string[] outputOptions = new string[] { outputPaddingOrientation };

        string[] args = new string[] {
            flags[0],
            inputFormat,
            "--from-options=" + inputEndianity,
            flags[1],
            outputFormat,
            "--to-options=" + outputPaddingOrientation,
            flags[2],
            inputPath,
            flags[3],
            outputPath,
            flags[4],
            delimiter.ToString(),
            flags[5]
        };

        Structs.Arguments arguments = ArgumentsParser.Parse(args);

        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.inputOptions.ToArray(), inputOptions));
        Assert.IsTrue(ArrayUtils.Compare<string>(arguments.outputOptions.ToArray(), outputOptions));
        Assert.AreEqual(arguments.inputFormat, Enums.Format.Int);
        Assert.AreEqual(arguments.outputFormat, Enums.Format.Bits);
        Assert.AreEqual(arguments.inputPath, inputPath);
        Assert.AreEqual(arguments.outputPath, outputPath);
        Assert.AreEqual(arguments.delimiter, delimiter);

        Assert.IsTrue(arguments.help);
    }
}
