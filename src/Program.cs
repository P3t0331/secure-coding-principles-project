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
                return 0;
            }

            CLI.InputProcessor inputProcessor = new CLI.InputProcessor(cliArgs);
            inputProcessor.ProcessInput();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            CLI.Help.Print();
            return 1;
        }
        catch (Exception e) when (e is FormatException || e is NotImplementedException)
        {
            Console.WriteLine(e.Message);
            return 1;
        }
        catch (Exception)
        {
            Console.WriteLine("Unknown exception occured. This application will terminate now.");
            return 1;
        }

        return 0;
    }
}