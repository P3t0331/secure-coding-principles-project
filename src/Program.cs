namespace Panbyte;
class Program
{
    static int Main(string[] args)
    {
        try
        {
            Structs.Arguments cliArgs = CLI.ArgumentsParser.Parse(args);

            if (cliArgs.help)
            {
                CLI.Help.Print();
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            CLI.Help.Print();
            return 1;
        }

        return 0;
    }
}