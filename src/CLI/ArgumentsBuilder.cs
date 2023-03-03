namespace Panbyte.CLI {
    public class ArgumentsBuilder {
        private string? inputFormat = null;
        public string? inputOptions;
        private string? outputFormat = null;
        public string? outputOptions;
        private string? inputPath = null;
        private string? outputPath = null;
        private char? delimiter = null;
        private Boolean help = false;

        public ArgumentsBuilder WithInputFormat(string? inputFormat) {
            this.inputFormat = inputFormat;
            return this;
        }

        public ArgumentsBuilder WithInputOptions(string? inputOptions) {
            this.inputOptions = inputOptions;
            return this;
        }

        public ArgumentsBuilder WithOutputFormat(string? outputFormat) {
            this.outputFormat = outputFormat;
            return this;
        }

        public ArgumentsBuilder WithOutputOptions(string? outputOptions) {
            this.outputOptions = outputOptions;
            return this;
        }

        public ArgumentsBuilder WithInputPath(string? inputPath) {
            this.inputPath = inputPath;
            return this;
        }

        public ArgumentsBuilder WithOutputPath(string? outputPath) {
            this.outputPath = outputPath;
            return this;
        }

        public ArgumentsBuilder WithDelimiter(char delimiter) {
            this.delimiter = delimiter;
            return this;
        }

        public ArgumentsBuilder WithHelp(Boolean help) {
            this.help = help;
            return this;
        }

        public Arguments Build() {
            return new Arguments(inputFormat, inputOptions, outputFormat, outputOptions, inputPath, outputPath, delimiter, help);
        }
    }
}
