namespace Panbyte.CLI {
    public class Help {
        private const string HelpText = @"
ARGS:
    -f FORMAT       --from=FORMAT           Set input data format
                    --from-options=OPTIONS  Set input options
    -t FORMAT       --to=FORMAT             Set output data format
                    --to-options=OPTIONS    Set output options
    -i FILE         --input=FILE            Set input file (default stdin)
    -o FILE         --output=FILE           Set output file (default stdout)
    -d DELIMITER    --delimiter=DELIMITER   Record delimiter (default newline)
    -h              --help                  Print help

FORMATS:
    bytes   Raw bytes
    hex     Hex-encoded string
    int     Integer
    bits    0,1-represented bits
    array   Byte array
    
";

        public static void Print() {
            Console.Write(HelpText);
        }
    }
}