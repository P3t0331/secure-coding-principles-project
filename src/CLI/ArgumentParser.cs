namespace Panbyte.CLI;

public class ArgumentsParser
{
    public static Structs.Arguments Parse(string[] args)
    {
        ArgumentsBuilder builder = new ArgumentsBuilder();

        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];

            if (arg == "-f" || arg == "--from")
            {
                builder.WithInputFormat(FormatParser.Parse(GetNextArg(args, ref i)));
            }
            else if (arg.Contains("--from-options="))
            {
                var option = GetOption(arg);
                builder.WithInputOptions(option);
            }
            else if (arg == "-t" || arg == "--to")
            {
                builder.WithOutputFormat(FormatParser.Parse(GetNextArg(args, ref i)));
            }
            else if (arg.Contains("--to-options="))
            {
                var option = GetOption(arg);
                builder.WithOutputOptions(option);
            }
            else if (arg == "-i" || arg == "--input")
            {
                builder.WithInputPath(GetNextArg(args, ref i));
            }
            else if (arg == "-o" || arg == "--output")
            {
                builder.WithOutputPath(GetNextArg(args, ref i));
            }
            else if (arg == "-d" || arg == "--delimiter")
            {
                builder.WithDelimiter(GetNextArg(args, ref i));
            }
            else if (arg == "-h" || arg == "--help")
            {
                builder.WithHelp(true);
            }
            else
            {
                throw new ArgumentException("Unknown argument: " + arg);
            }
        }

        return builder.Build();
    }

    private static string GetNextArg(string[] args, ref int i)
    {
        if (++i < args.Length)
        {
            return args[i];
        }
        else
        {
            throw new ArgumentException("Missing argument value for: " + args[i - 1]);
        }
    }

    private static string GetOption(string arg)
    {
        var argSplit = arg.Split('=');
        var option = argSplit[1];
        if (argSplit.Length > 2)
        {
            throw new ArgumentException("Option not recognized " + arg);
        }
        if (string.IsNullOrEmpty(option))
        {
            throw new ArgumentException("No option provided: " + arg);
        }
        return option;
    }
}