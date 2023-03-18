using Panbyte.Convertors;

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
            string? stdinInput; //we do not support files yet.
            while ((stdinInput = Console.ReadLine()) != null)
            {
                var inputs = stdinInput.Split(cliArgs.delimiter);
                foreach (var input in inputs)
                {
                    //BYTES INPUT
                    if (cliArgs.inputFormat == Enums.Format.Bytes)
                    {
                        ProcessInput(cliArgs, new BytesConvertor(input));
                    }

                    //HEX INPUT
                    else if (cliArgs.inputFormat == Enums.Format.Hex)
                    {
                        ProcessInput(cliArgs, new HexConvertor(input));
                    }

                    //INT INPUT
                    else if (cliArgs.inputFormat == Enums.Format.Int)
                    {
                        IntConvertor convertor;
                        convertor = GetIntConvertor(cliArgs, input);
                        ProcessInput(cliArgs, convertor);
                    }
                    else
                    {
                        throw new ArgumentException("Argument not recognized (yet): " + cliArgs.inputFormat);
                    }
                }
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
            CLI.Help.Print();
            return 1;
        }
        catch (FormatException e)
        {
            Console.WriteLine(e.Message);
            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine("Unknown exception occured. This application will terminate now.\nException: " + e.Message);
        }

        return 0;
    }

    private static string ConvertToIntParser(Structs.Arguments cliArgs, IConvertor convertor)
    {
        var option = cliArgs.outputOptions.First();
        if (Enum.TryParse(option, true, out Enums.Endianity endianity))
        {
            return convertor.ConvertToInt(endianity);
        }
        else
        {
            throw new ArgumentException("Only 'little' or 'big' is allowed as option");
        }
    }

    private static void ProcessInput(Structs.Arguments cliArgs, IConvertor inputConvertor)
    {
        var convertor = inputConvertor;
        if (cliArgs.outputFormat == Enums.Format.Bytes)
        {
            Console.WriteLine(convertor.ConvertToBytes());
        }
        else if (cliArgs.outputFormat == Enums.Format.Hex)
        {
            Console.WriteLine(convertor.ConvertToHex());
        }
        else if (cliArgs.outputFormat == Enums.Format.Int)
        {
            if (!cliArgs.outputOptions.Any())
            {
                Console.WriteLine(convertor.ConvertToInt());
            }
            else
            {
                Console.WriteLine(ConvertToIntParser(cliArgs, convertor));
            }
        }
        else
        {
            throw new ArgumentException("Argument not recognized(yet): " +  cliArgs.outputFormat);
        }

    }


    private static IntConvertor GetIntConvertor(Structs.Arguments cliArgs, string input)
    {
        IntConvertor convertor = new IntConvertor(input);
        if (cliArgs.inputOptions.Any())
        {
            var option = cliArgs.inputOptions.First();
            if (Enum.TryParse(option, true, out Enums.Endianity endianity))
            {
                convertor = new IntConvertor(input, endianity);
            }
            else
            {
                throw new ArgumentException("Only 'little' or 'big' is allowed as option");
            }

        }
        return convertor;

    }
}