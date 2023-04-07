namespace Panbyte.Utils;

public static class ByteArrayUtils
{

    private const string BRACKET_REGEX = @"'[\[\]\{\}\(\)]'|''";

    public static string appendBrackets(string input, Structs.ArrayOptions options)
    {
        switch (options.bracket)
        {
            case Enums.Bracket.Curly:
                return $"{{{input}}}";
            case Enums.Bracket.Round:
                return $"({input})";
            case Enums.Bracket.Square:
                return $"[{input}]";
            default:
                throw new Exception("Unknown bracket option: " + options.bracket);
        }
    }

    public static string removeOuterBrackets(string input)
    {
        if ((input.StartsWith('{') || input.StartsWith('[') || input.StartsWith('(')) &&
            (input.EndsWith('}') || input.EndsWith(']') || input.EndsWith(')')))
        {
            input = input.Substring(1, input.Length - 2);
        }

        return input;
    }

    public static string extractBrackets(in string input)
    {
        // Remove any character that is not a bracket or an apostrophe
        return String.Concat((input.Where((c) => "()[]{}'".Contains(c))));
    }
    public static string removeBracketsInApostrophes(in string input)
    {

        // Remove brackets enclosed in apostrophes
        return System.Text.RegularExpressions.Regex.Replace(input, BRACKET_REGEX, "");

    }

    public static string[] splitArray(string input)
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