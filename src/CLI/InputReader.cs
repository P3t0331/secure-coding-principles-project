using System.Text;

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

    public string? ReadUntilDelimiter(String delimiter)
    {
        var sb = new StringBuilder();
        var reader = fileReader ?? Console.In;
        
        while (reader.Peek() >= 0)
        { 
            sb.Append((char)reader.Read()); 
            
            if (sb.Length > delimiter.Length && sb.ToString().EndsWith(delimiter))
            {
                return sb.ToString(); 
            }
        }
        
        return sb.ToString();
    }

    public void Close()
    {
        if (fileReader != null)
        {
            fileReader.Close();
        }
    }

    public bool DoesReaderHaveAdditionalInput()
    {
        var reader = fileReader ?? Console.In;
        return reader.Peek() >= 0;
    }
}