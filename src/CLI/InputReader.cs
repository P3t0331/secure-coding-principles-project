namespace Panbyte.CLI;

public class InputReader
{
    readonly StreamReader? fileReader;

    public InputReader(string? inputPath)
    {
        if (inputPath != null)
        {
            try
            {
                fileReader = new StreamReader(inputPath);
            }
            catch
            {
                throw new ArgumentException("Could not open input file: " + inputPath);
            }
        }
    }

    public string? ReadLine()
    {
        if (fileReader != null)
        {
            return fileReader.ReadLine();
        }
        else
        {
            return Console.ReadLine();
        }
    }

    public void Close()
    {
        if (fileReader != null)
        {
            fileReader.Close();
        }
    }
}