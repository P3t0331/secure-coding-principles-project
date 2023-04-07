using Panbyte.Convertors;
using Panbyte.Enums;
using Panbyte.Structs;
using Panbyte.Validators;
using Panbyte.Utils;
namespace Panbyte.CLI;

public class InputProcessor
{
    private readonly Structs.Arguments cliArgs;
    private readonly Convertor convertor = new Convertor();
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

        while ((inputLine = inputReader.ReadLine()) != null)
        {
            string[] inputs = inputLine.Split(cliArgs.delimiter);
            String outputLine = "";

            for (int i = 0; i < inputs.Length; i++)
            {
                if (cliArgs.inputFormat == Format.Array && cliArgs.outputFormat == Format.Array)
                {
                    ArrayValidator.CheckCorrectNesting(inputs[i]);
                    outputLine += ProcessNestedArray(inputs[i]);
                }
                else
                {
                    outputLine += ProcessLine(inputs[i]);
                }


                if (i != inputs.Length - 1 && cliArgs.delimiter != null)
                {
                    outputLine += cliArgs.delimiter;
                }
            }

            outputWriter.WriteLine(outputLine);
        }

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
                return InputConvertor.ConvertInt(uint.Parse(input), endianity);
            case Format.Bits:
                InputValidator.CheckIfBits(input);
                PaddingOrientation orientation = OptionsParser.ParsePadding(cliArgs.inputOptions);
                return InputConvertor.ConvertBits(input, orientation);
            case Format.Array:
                ArrayValidator.CheckCorrectNesting(input);
                return InputConvertor.ConvertArray(input, OptionsParser.ParseArrayOptions(cliArgs.inputOptions));
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
                return convertor.ConvertToBytes(bytes);
            case Format.Hex:
                return convertor.ConvertToHex(bytes);
            case Format.Int:
                Endianity endianity = OptionsParser.ParseEndianity(cliArgs.outputOptions);
                return convertor.ConvertToInt(bytes, endianity);
            case Format.Bits:
                return convertor.ConvertToBits(bytes);
            case Format.Array:
                ArrayOptions options = OptionsParser.ParseArrayOptions(cliArgs.outputOptions);
                return ByteArrayUtils.appendBrackets(convertor.ConvertToByteArray(bytes, options), options);
            default:
                throw new ArgumentException("Argument not recognized: " + cliArgs.outputFormat);
        }
    }

    private string ProcessNestedArray(string input)
    {
        string result = "";
        ArrayOptions options = OptionsParser.ParseArrayOptions(cliArgs.outputOptions);

        input = ByteArrayUtils.removeOuterBrackets(input);

        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        string[] elementList = ByteArrayUtils.splitArray(input);

        for (int i = 0; i < elementList.Length; i++)
        {
            if (!ArrayValidator.isNested(elementList[i]))
            {
                result += convertor.ConvertToByteArray(GetBytes(elementList[i]), options);
            }
            else
            {
                result += ProcessNestedArray(elementList[i]);
            }

            if (i != elementList.Length - 1)
            {
                result += ", ";
            }
        }

        return ByteArrayUtils.appendBrackets(result, options);
    }
}