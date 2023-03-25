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
echo "Your input" || dotnet run -- [ARGS...]
```
Replace ```[ARGS...]``` with the desired command-line arguments.

### Command-Line Arguments
The following command-line arguments are available:
```
-f FORMAT, --from=FORMAT              Set input data format
--from-options=OPTIONS                Set input options
-t FORMAT, --to=FORMAT                Set output data format
--to-options=OPTIONS                  Set output options
-i FILE, --input=FILE                 Set input file (default stdin) (not supported yet)
-o FILE, --output=FILE                Set output file (default stdout) (not supported yet)
-d DELIMITER, --delimiter=DELIMITER   Record delimiter (default newline) (not fully supported yet)
-h, --help                            Print help
```
### Formats
The following input and output data formats are available:
```
bytes: Raw bytes
hex: Hex-encoded string
int: Integer
bits: 0,1-represented bits (not supported yet)
array: Byte array (not supported yet)
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
```
