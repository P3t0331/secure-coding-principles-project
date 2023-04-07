# Panbyte
Panbyte is a tool for converting inputs from one format to another.
It supports multiple inputs at the same time and can handle invalid inputs securely. 
This project is written in C# using .NET 7.0.

## Setup
To run Panbyte, you will need to have .NET 7.0 installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).

## Installation
Clone the repository.

## Usage
To use Panbyte, open the terminal or command prompt and navigate to the Panbyte "src" directory. Run the following command:
```
echo "Your input" | dotnet run -- [ARGS...]
```
Replace ```[ARGS...]``` with the desired command-line arguments.

### Command-Line Arguments
The following command-line arguments are available:
```
-f FORMAT, --from=FORMAT              Set input data format
--from-options=OPTIONS                Set input options
-t FORMAT, --to=FORMAT                Set output data format
--to-options=OPTIONS                  Set output options
-i FILE, --input=FILE                 Set input file (default stdin)
-o FILE, --output=FILE                Set output file (default stdout)
-d DELIMITER, --delimiter=DELIMITER   Record delimiter (default newline)
-h, --help                            Print help
```
### Formats
The following input and output data formats are available:
```
bytes: Raw bytes
hex: Hex-encoded string
int: Integer
bits: 0,1-represented bits
array: Byte array
```
### Options
```
Format: int
Input Options:
• big – Store the integer in big-endian representation (most significant byte at the lowest addres;
default).
• little – Store the integer in little-endian representation (least significant byte at the lowest
address).

Output options:
• big – Interpret bytes as an an integer in big-endian representation (most significant byte at the
lowest addres; default).
• little – Interpret bytes as an integer in little-endian representation (least significant byte at the
lowest address).

Format: bits
Input Options:
• left – If necessary, pad input with zero bits from left (default)
• right – If necessary, pad input with zero bits from right.

Format: Array

Output options:
• 0x – Represent bytes as a 0x-prefixed hex number (e.g., 0xff; default).
• 0 – Represent bytes as a decimal number (e.g., 255).
• a – Represent bytes as characters (e.g., 'a', '\x00').
• 0b – Represent bytes as 0b-prefixed binary number (e.g., 0b11111111).
• { or } or {} – Use curly brackets in output (default).
• [ or ] or [] – Use square brackets in output.
• ( or ) or () – Use regular brackets in output.
```
## Examples
```
$ echo 1234567890 | dotnet run -- -f int --from-options=little -t hex
d2029649
$ echo 499602d2 | dotnet run -- -f hex -t int
1234567890
$ echo 499602d2 | dotnet run -- -f hex -t int --to-options=big
1234567890
$ echo d2029649 | dotnet run -- -f hex -t int --to-options=little
1234567890

$ echo 100 1111 0100 1011 | dotnet run -- -f bits -t bytes
OK
$ echo 100111101001011 | dotnet run -- -f bits --from-options=left -t bytes
OK
$ echo 100111101001011 | dotnet run -- -f bits --from-options=right -t hex
9e96
$ echo OK | dotnet run -- -f bytes -t bits
0100111101001011

$ echo 01020304 | dotnet run -- -f hex -t array
{0x1, 0x2, 0x3, 0x4}
$ echo "{0x01, 2, 0b11, '\x04'}" | dotnet run -- -f array -t hex
01020304
$ echo "{0x01,2,0b11 ,'\x04' }" | dotnet run -- -f array -t array
{0x1, 0x2, 0x3, 0x4}
$ echo "[0x01, 2, 0b11, '\x04']" | dotnet run -- -f array -t array --to-options=0x
{0x1, 0x2, 0x3, 0x4}
$ echo "(0x01, 2, 0b11, '\x04')" | dotnet run -- -f array -t array --to-options=0
{1, 2, 3, 4}
$ echo "{0x01, 2, 0b11, '\x04'}" | dotnet run -- -f array -t array --to-options=a
{'\x01', '\x02', '\x03', '\x04'}
$ echo "[0x01, 2, 0b11, '\x04']" | dotnet run -- -f array -t array --to-options=0b
{0b1, 0b10, 0b11, 0b100}
$ echo "(0x01, 2, 0b11, '\x04')" | dotnet run -- -f array -t array --to-options="("
(0x1, 0x2, 0x3, 0x4)
$ echo "{0x01, 2, 0b11, '\x04'}" | dotnet run -- -f array -t array --to-options=0 \
--to-options="["
[1, 2, 3, 4]

Examples with nesting (applicable only in array-array conversions):
$ echo "[[1, 2], [3, 4], [5, 6]]" | dotnet run -- -f array -t array
{{0x1, 0x2}, {0x3, 0x4}, {0x5, 0x6}}
$ echo "[[1, 2], [3, 4], [5, 6]]" | dotnet run -- -f array -t array \
--to-options="{" --to-options=0
{{1, 2}, {3, 4}, {5, 6}}
$ echo "{{0x01, (2), [3, 0b100, 0x05], '\x06'}}" | dotnet run -- -f array -t array \
--to-options=0 --to-options="["
[[1, [2], [3, 4, 5], 6]]
$ echo "()" | dotnet run -- -f array -t array
{}
$ echo "([],{})" | dotnet run -- -f array -t array --to-options="["
[[], []]
```
