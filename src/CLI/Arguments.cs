namespace Panbyte.CLI {
    public readonly struct Arguments {
        public readonly string? inputFormat;
        public readonly string? inputOptions;
        public readonly string? outputFormat;
        public readonly string? outputOptions;
        public readonly string? inputPath;
        public readonly string? outputPath;
        public readonly char? delimiter;
        public readonly Boolean help;

        public Arguments(string? inputFormat, string? inputOptions, string? outputFormat, string? outputOptions, string? inputPath, string? outputPath, char? delimiter, Boolean help) {
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
}
