using Panbyte.Convertors;
using Panbyte.Enums;
using Panbyte.Structs;
using Panbyte.Validators;
using Panbyte.Utils;
using System.Text;
using System.Numerics;

namespace Panbyte.CLI;

public class InputProcessor
{
    private readonly Structs.Arguments cliArgs;
    private readonly InputReader inputReader;
    private readonly OutputWriter outputWriter;

    public InputProcessor(Structs.Arguments cliArgs)
    {
        this.cliArgs = cliArgs;
        this.inputReader = new InputReader(cliArgs.inputPath);
        this.outputWriter = new OutputWriter(cliArgs.outputPath);
    }

    public void ProcessInput()
    {
        string? inputLine;
        var delimiter = cliArgs.delimiter ?? Environment.NewLine;

        while (inputReader.DoesReaderHaveAdditionalInput() && (inputLine = inputReader.ReadUntilDelimiter(delimiter)) != null)
        {
            string input = inputLine.TrimEnd(delimiter.ToCharArray());
            StringBuilder outputLine = new StringBuilder();

            if (cliArgs.inputFormat == Format.Array && cliArgs.outputFormat == Format.Array)
            {
                ArrayValidator.CheckCorrectNesting(input);
                ArrayValidator.CheckValidPosition(input);
                outputLine.Append(ProcessNestedArray(input));
            }
            else
            {
                outputLine.Append(ProcessLine(input));
            }

            if (inputReader.DoesReaderHaveAdditionalInput())
            {
                outputLine.Append(delimiter);
            }

            outputWriter.Write(outputLine.ToString());
        }

        outputWriter.Write(Environment.NewLine);
        inputReader.Close();
        outputWriter.Close();
    }


    private byte[] GetBytes(string input)
    {
        switch (cliArgs.inputFormat)
        {
            case Format.Bytes:
                return InputConvertor.ConvertBytes(input);
            case Format.Hex:
                InputValidator.CheckIfHex(input);
                return InputConvertor.ConvertHex(input);
            case Format.Int:
                InputValidator.CheckIfUint(input);
                Endianity endianity = OptionsParser.ParseEndianity(cliArgs.inputOptions);
                return InputConvertor.ConvertInt(BigInteger.Parse(input), endianity);
            case Format.Bits:
                InputValidator.CheckIfBits(input);
                PaddingOrientation orientation = OptionsParser.ParsePadding(cliArgs.inputOptions);
                return InputConvertor.ConvertBits(input, orientation);
            case Format.Array:
                ArrayValidator.CheckCorrectNesting(input);
                return InputConvertor.ConvertArray(input);
            default:
                throw new ArgumentException("Argument not recognized: " + cliArgs.inputFormat);
        }
    }

    private string ProcessLine(string line)
    {
        byte[] bytes = GetBytes(line);

        switch (cliArgs.outputFormat)
        {
            case Format.Bytes:
                return Convertor.ConvertToBytes(bytes);
            case Format.Hex:
                return Convertor.ConvertToHex(bytes);
            case Format.Int:
                Endianity endianity = OptionsParser.ParseEndianity(cliArgs.outputOptions);
                return Convertor.ConvertToInt(bytes, endianity);
            case Format.Bits:
                return Convertor.ConvertToBits(bytes);
            case Format.Array:
                ArrayOptions options = OptionsParser.ParseArrayOptions(cliArgs.outputOptions);
                return ByteArrayUtils.appendBrackets(Convertor.ConvertToByteArray(bytes, options), options);
            default:
                throw new ArgumentException("Argument not recognized: " + cliArgs.outputFormat);
        }
    }

    private string ProcessNestedArray(string input)
    {
        StringBuilder result = new StringBuilder();
        ArrayOptions options = OptionsParser.ParseArrayOptions(cliArgs.outputOptions);

        input = ByteArrayUtils.removeOuterBrackets(input);

        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        string[] elementList = ByteArrayUtils.splitArray(input);

        for (int i = 0; i < elementList.Length; i++)
        {
            if (!ArrayValidator.IsNested(elementList[i]))
            {
                result.Append(Convertor.ConvertToByteArray(GetBytes(elementList[i]), options));
            }
            else
            {
                result.Append(ProcessNestedArray(elementList[i]));
            }

            if (i != elementList.Length - 1)
            {
                result.Append(", ");
            }
        }

        return ByteArrayUtils.appendBrackets(result.ToString(), options);
    }
}