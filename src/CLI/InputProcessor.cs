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
                return convertor.ConvertToByteArray(bytes, options);
            default:
                throw new ArgumentException("Argument not recognized: " + cliArgs.outputFormat);
        }
    }

    private string ProcessNestedArray(string input)
    {
        string result = "";
        ArrayOptions options = OptionsParser.ParseArrayOptions(cliArgs.outputOptions);

        input = removeOuterBrackets(input);

        input = String.Concat(input.Where(c => !Char.IsWhiteSpace(c)));
        string[] elementList = splitArray(input);

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

        switch (options.bracket)
        {
            case Enums.Bracket.Curly:
                return $"{{{result}}}";
            case Enums.Bracket.Round:
                return $"({result})";
            case Enums.Bracket.Square:
                return $"[{result}]";
            default:
                throw new Exception("Unknown bracket option: " + options.bracket);
        }
    }

    private string removeOuterBrackets(string input)
    {
        if ((input.StartsWith('{') || input.StartsWith('[') || input.StartsWith('(')) &&
            (input.EndsWith('}') || input.EndsWith(']') || input.EndsWith(')')))
        {
            input = input.Substring(1, input.Length - 2);
        }

        return input;
    }

    private string[] splitArray(string input)
    {
        List<string> result = new List<string> { };
        int indexOfLastComma = -1;
        int openCount = 0;
        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '{':
                case '[':
                case '(':
                    openCount++;
                    break;
                case '}':
                case ']':
                case ')':
                    openCount--;
                    break;
                case ',':
                    if (openCount == 0)
                    {
                        result.Add(input.Substring(indexOfLastComma + 1, i - indexOfLastComma - 1));
                        indexOfLastComma = i;
                    };
                    break;
                default:
                    break;
            }
        }

        result.Add(input.Substring(indexOfLastComma + 1, input.Length - indexOfLastComma - 1));
        return result.ToArray();
    }
}