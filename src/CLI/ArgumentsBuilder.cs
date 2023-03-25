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
    private bool help = false;

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

    public ArgumentsBuilder WithHelp(Boolean help)
    {
        this.help = help;
        return this;
    }

    public Structs.Arguments Build()
    {
        return new Structs.Arguments(inputFormat, inputOptions, outputFormat, outputOptions, inputPath, outputPath, delimiter, help);
    }
}
