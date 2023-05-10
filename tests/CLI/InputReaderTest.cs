using Panbyte.CLI;

namespace tests.CLI
{
    [TestClass]
    public class InputReaderTest
    {
        [TestMethod]
        public void InputReaderEndsOnDelimiter()
        {
            string input = "Hello World!";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);

            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            InputReader inputReader = new(null);

            var result = inputReader.ReadUntilDelimiter("World");
            Assert.AreEqual("Hello World", result);
        }

        [TestMethod]
        public void InputReaderReadsAllOnNoDelimiterIncluded()
        {
            string input = "Hello World!";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);

            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            InputReader inputReader = new(null);

            var result = inputReader
                .ReadUntilDelimiter("I am not inside that string for sure.");
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void InputReaderHasAdditionalInputOnNonEmptyInput()
        {
            string input = "Hello World!";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);

            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            InputReader inputReader = new(null);

            var result = inputReader.DoesReaderHaveAdditionalInput();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void InputReaderHasAdditionalInputOnEmptyInput()
        {
            string input = "";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);

            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            InputReader inputReader = new(null);

            var result = inputReader.DoesReaderHaveAdditionalInput();
            Assert.IsFalse(result);
        }
    }
}
