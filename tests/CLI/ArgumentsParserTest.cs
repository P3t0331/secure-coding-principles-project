using Panbyte.CLI;

namespace Panbyte.Tests.CLI {
    [TestClass]
    public class ArgumentsParserTest
    {
        [TestMethod]
        public void TestHelp()
        {
            string[] args = new string[] { "-h" };
            Arguments arguments = ArgumentsParser.Parse(args);

            Assert.IsTrue(arguments.help);
        }

        [TestMethod]
        public void TestNoHelp()
        {
            string[] args = new string[] {};
            Arguments arguments = ArgumentsParser.Parse(args);

            Assert.IsFalse(arguments.help);
        }
    }
}
