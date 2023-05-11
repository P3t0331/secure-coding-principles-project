using Panbyte.CLI;
using Panbyte.Structs;

namespace tests.CLI
{
    [TestClass]
    public class InputProcessorTest
    {
        [TestMethod]
        public void InputProcessorProcessesInputWithSetDelimiter()
        {
            Arguments arguments = new Arguments(Panbyte.Enums.Format.Int,
                new(), Panbyte.Enums.Format.Bits, new(), null, null, "x");
            string input = "1x2";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            InputProcessor inputProcessor = new(arguments);

            inputProcessor.ProcessInput();

            Assert.AreEqual("00000001x00000010" + Environment.NewLine, stringWriter.ToString());
        }

        [TestMethod]
        public void InputProcessorProcessesInputNoDelimiterDefaultsNewLine()
        {
            Arguments arguments = new Arguments(Panbyte.Enums.Format.Int,
                new(), Panbyte.Enums.Format.Bits, new(), null, null, null);
            string input = "1" + Environment.NewLine + "2";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            InputProcessor inputProcessor = new(arguments);

            inputProcessor.ProcessInput();

            Assert.AreEqual("00000001" + Environment.NewLine + "00000010" + Environment.NewLine, stringWriter.ToString());
        }

        [TestMethod]
        public void InputProcessorProcessesInputDelimiterNotInInput()
        {
            Arguments arguments = new Arguments(Panbyte.Enums.Format.Int,
                new(), Panbyte.Enums.Format.Int, new(), null, null, null);
            string input = "12";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);
            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);
            InputProcessor inputProcessor = new(arguments);

            inputProcessor.ProcessInput();

            Assert.AreEqual("12" + Environment.NewLine, stringWriter.ToString());
        }
    }
}
