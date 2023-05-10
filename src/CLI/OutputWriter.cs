namespace Panbyte.CLI;

public class OutputWriter
{
    readonly private StreamWriter? fileWriter;

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

    public void Write(string line)
    {
        var writer = fileWriter ?? Console.Out;

        writer.Write(line);
    }

    public void Close()
    {
        if (fileWriter != null)
        {
            fileWriter.Close();
        }
    }
}