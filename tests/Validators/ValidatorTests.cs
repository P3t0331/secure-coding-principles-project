namespace Panbyte.tests.Validators;

using Panbyte.Validators;
[TestClass]
public class ValidatorTests
{
    [TestMethod]
    public void TestUintInputValid()
    {
        string input = "3124535";
        bool result = InputValidator.CheckIfUint(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestUintInputNegativeNumber()
    {
        string input = "-42342";
        bool result = InputValidator.CheckIfUint(input);
        Assert.IsFalse(result);

    }

    [TestMethod]
    public void TestUintInputOverflow()
    {
        string input = "10156543167";
        bool result = InputValidator.CheckIfUint(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestUintInputNotNumber()
    {
        string input = "1123gjwog443";
        bool result = InputValidator.CheckIfUint(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestUintInputEmpty()
    {
        string input = "";
        bool result = InputValidator.CheckIfUint(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestHexInputValid()
    {
        string input = "432aa42b";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestHexInputNotHex()
    {
        string input = "432aa42bxx";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestHexInputNotHexOdd()
    {
        string input = "432aa42bb";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestHexInputHexSpaces()
    {
        string input = "43 2a a4 2b aa";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestHexInputHexSpacesMultiple()
    {
        string input = "43  2a      a4  2b   aa";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestHexInputEmpty()
    {
        string input = "";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestHexInputSpaceOnly()
    {
        string input = " ";
        bool result = InputValidator.CheckIfHex(input);
        Assert.IsFalse(result);
    }
}
