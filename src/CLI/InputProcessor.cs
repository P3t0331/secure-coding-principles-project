using Panbyte.Convertors;
using Panbyte.Enums;
namespace Panbyte.CLI;

public class InputProcessor
{
    private readonly Structs.Arguments cliArgs;
    private readonly IConvertor convertor;
    private readonly InputReader inputReader;
    private readonly OutputWriter outputWriter;

    public InputProcessor(Structs.Arguments cliArgs)
    {
        this.cliArgs = cliArgs;
        this.convertor = ConvertorFactory.CreateConvertor(cliArgs);
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
                outputLine += ProcessLine(inputs[i], convertor);

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

    private string ProcessLine(string line, IConvertor convertor)
    {
        switch (cliArgs.outputFormat)
        {
            case Format.Bytes:
                return convertor.ConvertToBytes(line);
            case Format.Hex:
                return convertor.ConvertToHex(line);
            case Format.Int:
                return convertor.ConvertToInt(line, IntConvertor.ParseEndianity(cliArgs.outputOptions));
            case Format.Bits:
                return convertor.ConvertToBits(line);
            case Format.Array:
                return convertor.ConvertToByteArray(line);
            default:
                throw new ArgumentException("Argument not recognized (yet): " + cliArgs.outputFormat);
        }
    }
}