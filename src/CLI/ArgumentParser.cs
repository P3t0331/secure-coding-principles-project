namespace Panbyte.CLI {
    public class ArgumentsParser {
        public static Arguments Parse(string[] args) {
            ArgumentsBuilder builder = new ArgumentsBuilder();

            for (int i = 0; i < args.Length; i++) {
                string arg = args[i];

                if (arg == "-f" || arg == "--from") {
                    builder.WithInputFormat(GetNextArg(args, ref i));
                } else if (arg == "--from-options") {
                    builder.WithInputOptions(GetNextArg(args, ref i));
                } else if (arg == "-t" || arg == "--to") {
                    builder.WithOutputFormat(GetNextArg(args, ref i));
                } else if (arg == "--to-options") {
                    builder.WithOutputOptions(GetNextArg(args, ref i));
                } else if (arg == "-i" || arg == "--input") {
                    builder.WithInputPath(GetNextArg(args, ref i));
                } else if (arg == "-o" || arg == "--output") {
                    builder.WithOutputPath(GetNextArg(args, ref i));
                } else if (arg == "-d" || arg == "--delimiter") {
                    builder.WithDelimiter(GetNextArg(args, ref i)[0]);
                } else if (arg == "-h" || arg == "--help") {
                    builder.WithHelp(true);
                } else {
                    throw new ArgumentException("Unknown argument: " + arg);
                }
            }

            return builder.Build();
        }

        private static string GetNextArg(string[] args, ref int i) {
            if (++i < args.Length) {
                return args[i];
            } else {
                throw new ArgumentException("Missing argument value for: " + args[i - 1]);
            }
        }
    }
}