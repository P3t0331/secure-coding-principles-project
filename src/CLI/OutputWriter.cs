public class OutputWriter
{
    private StreamWriter? fileWriter;

    public OutputWriter(string? outputPath)
    {
        if (outputPath != null)
        {
            try
            {
                fileWriter = new StreamWriter(outputPath);
            }
            catch
            {
                throw new ArgumentException("Could not open output file: " + outputPath);
            }
        }
    }

    public void WriteLine(string line)
    {
        if (fileWriter != null)
        {
            fileWriter.WriteLine(line);
        }
        else
        {
            Console.WriteLine(line);
        }
    }

    public void Close()
    {
        if (fileWriter != null)
        {
            fileWriter.Close();
        }
    }
}