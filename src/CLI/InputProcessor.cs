using Panbyte.Convertors;
using Panbyte.Enums;
using Panbyte.Structs;
using Panbyte.Validators;
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
                outputLine += ProcessLine(inputs[i]);

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
                ArrayValidator.isNested(input);
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
                return convertor.ConvertToByteArray(bytes, options);
            default:
                throw new ArgumentException("Argument not recognized: " + cliArgs.outputFormat);
        }
    }
}