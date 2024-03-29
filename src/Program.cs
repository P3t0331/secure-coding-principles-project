namespace Panbyte;
static class Program
{
    static int Main(string[] args)
    {
        try
        {
            Structs.Arguments cliArgs = CLI.ArgumentsParser.Parse(args);
            CLI.InputProcessor inputProcessor = new CLI.InputProcessor(cliArgs);
            inputProcessor.ProcessInput();
        }
        catch (Exceptions.HelpException)
        {
            CLI.Help.Print();
            return 0;
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