using Panbyte.CLI;

namespace tests.CLI
{
    [TestClass]
    public class OutputWriterTest
    {
        [TestMethod]
        public void InputReaderEndsOnDelimiter()
        {
            string input = "Hello World!";

            StringReader stringReader = new(input);
            Console.SetIn(stringReader);

            StringWriter stringWriter = new();
            Console.SetOut(stringWriter);

            OutputWriter outputWriter = new(null);

            outputWriter.Write(input);
            Assert.AreEqual(input, stringWriter.ToString());
        }
    }
}
