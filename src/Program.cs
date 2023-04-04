using panbyte.Validators;
using Panbyte.Convertors;
using Panbyte.Enums;
using System.Text;

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
            string? stdinInput; //we do not support files yet.
            while ((stdinInput = Console.ReadLine()) != null)
            {
                var inputs = stdinInput.Split(cliArgs.delimiter);
                foreach (var input in inputs)
                {
                    //BYTES INPUT
                    if (cliArgs.inputFormat == Format.Bytes)
                    {
                        ProcessInput(cliArgs, new Convertor(InputConvertor.ConvertBytes(input)));
                    }

                    //HEX INPUT
                    else if (cliArgs.inputFormat == Format.Hex)
                    {
                        if (InputValidator.CheckIfHex(input))
                        {
                            ProcessInput(cliArgs, new Convertor(InputConvertor.ConvertHex(input)));
                        }
                        else
                        {
                            return 1;
                        }
                        
                    }

                    //INT INPUT
                    else if (cliArgs.inputFormat == Format.Int)
                    {
                        if (InputValidator.CheckIfUint(input))
                        {
                            Endianity endianity = Endianity.Big;
                            if (cliArgs.inputOptions.Any())
                            {
                                var option = cliArgs.inputOptions.First();
                                if (!Enum.TryParse(option, true, out endianity))
                                {
                                    throw new ArgumentException("Only 'little' or 'big' is allowed as option");
                                }

                            }
                            ProcessInput(cliArgs, new Convertor(InputConvertor.ConvertInt(uint.Parse(input), endianity)));
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else if (cliArgs.inputFormat == Format.Bits)
                    {
                        // TODO
                        throw new NotImplementedException("Format not supported yet");
                    }
                    else if (cliArgs.inputFormat == Format.Array)
                    {
                        // TODO
                        throw new NotImplementedException("Format not supported yet");
                    }
                    else
                    {
                        throw new ArgumentException("Argument not recognized: " + cliArgs.inputFormat);
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
        catch (NotImplementedException e)
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

    private static string ConvertToIntParser(Structs.Arguments cliArgs, Convertor convertor)
    {
        var option = cliArgs.outputOptions.First();
        if (Enum.TryParse(option, true, out Endianity endianity))
        {
            return convertor.ConvertToInt(endianity);
        }
        else
        {
            throw new ArgumentException("Only 'little' or 'big' is allowed as option");
        }
    }

    private static void ProcessInput(Structs.Arguments cliArgs, Convertor convertor)
    {
        if (cliArgs.outputFormat == Format.Bytes)
        {
            Console.WriteLine(convertor.ConvertToBytes());
        }
        else if (cliArgs.outputFormat == Format.Hex)
        {
            Console.WriteLine(convertor.ConvertToHex());
        }
        else if (cliArgs.outputFormat == Format.Int)
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
        else if (cliArgs.outputFormat == Format.Bits)
        {
            // TODO
            throw new NotImplementedException("Format not supported yet");
        }
        else if (cliArgs.outputFormat == Format.Array)
        {
            // TODO
            throw new NotImplementedException("Format not supported yet");
        }
        else
        {
            throw new ArgumentException("Argument not recognized: " +  cliArgs.outputFormat);
        }
    }
}