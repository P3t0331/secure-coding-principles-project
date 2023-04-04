namespace Panbyte.CLI;

public class ArgumentsBuilder
{
    private Enums.Format? inputFormat = null;
    private List<string> inputOptions = new List<string>();
    private Enums.Format? outputFormat = null;
    private List<string> outputOptions = new List<string>();
    private string? inputPath = null;
    private string? outputPath = null;
    private string? delimiter = null;

    public ArgumentsBuilder WithInputFormat(Enums.Format inputFormat)
    {
        this.inputFormat = inputFormat;
        return this;
    }

    public ArgumentsBuilder WithInputOptions(string inputOptions)
    {
        this.inputOptions.Add(inputOptions);
        return this;
    }

    public ArgumentsBuilder WithOutputFormat(Enums.Format outputFormat)
    {
        this.outputFormat = outputFormat;
        return this;
    }

    public ArgumentsBuilder WithOutputOptions(string outputOptions)
    {
        this.outputOptions.Add(outputOptions);
        return this;
    }

    public ArgumentsBuilder WithInputPath(string inputPath)
    {
        this.inputPath = inputPath;
        return this;
    }

    public ArgumentsBuilder WithOutputPath(string outputPath)
    {
        this.outputPath = outputPath;
        return this;
    }

    public ArgumentsBuilder WithDelimiter(string delimiter)
    {
        this.delimiter = delimiter;
        return this;
    }

    public Structs.Arguments Build()
    {
        if (inputFormat is null)
        {
            throw new ArgumentNullException("Input format is not set!");
        }
        else if (outputFormat is null)
        {
            throw new ArgumentNullException("Output format is not set!");
        }

        return new Structs.Arguments(GetInputFormat(), inputOptions, GetOutputFormat(), outputOptions, inputPath, outputPath, delimiter);
    }

    private Enums.Format GetInputFormat()
    {
        if (inputFormat is not null)
        {
            return (Enums.Format)inputFormat;
        }
        else
        {
            throw new ArgumentNullException("Input format is not set!");
        }
    }

    private Enums.Format GetOutputFormat()
    {
        if (outputFormat is not null)
        {
            return (Enums.Format)outputFormat;
        }
        else
        {
            throw new ArgumentNullException("Output format is not set!");
        }
    }
}
