namespace Panbyte.Structs;

public readonly struct Arguments
{
    public readonly Enums.Format? inputFormat;
    public readonly List<string> inputOptions;
    public readonly Enums.Format? outputFormat;
    public readonly List<string> outputOptions;
    public readonly string? inputPath;
    public readonly string? outputPath;
    public readonly string? delimiter;
    public readonly bool help;

    public Arguments(
        Enums.Format? inputFormat,
        List<string> inputOptions,
        Enums.Format? outputFormat,
        List<string> outputOptions,
        string? inputPath,
        string? outputPath,
        string? delimiter,
        bool help)
    {
        this.inputFormat = inputFormat;
        this.inputOptions = inputOptions;
        this.outputFormat = outputFormat;
        this.outputOptions = outputOptions;
        this.inputPath = inputPath;
        this.outputPath = outputPath;
        this.delimiter = delimiter;
        this.help = help;
    }
}
