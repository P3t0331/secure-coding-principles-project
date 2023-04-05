namespace Panbyte.tests.Validators;

using Panbyte.Validators;
[TestClass]
public class ValidatorTests
{
    [TestMethod]
    public void TestUintInputValid()
    {
        string input = "3124535";
        InputValidator.CheckIfUint(input);
    }

    [TestMethod]
    public void TestUintInputNegativeNumber()
    {
        string input = "-42342";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestUintInputOverflow()
    {
        string input = "10156543167";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestUintInputNotNumber()
    {
        string input = "1123gjwog443";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestUintInputEmpty()
    {
        string input = "";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestHexInputValid()
    {
        string input = "432aa42b";
        InputValidator.CheckIfHex(input);
    }

    [TestMethod]
    public void TestHexInputNotHex()
    {
        string input = "432aa42bxx";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestHexInputNotHexOdd()
    {
        string input = "432aa42bb";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestHexInputHexSpaces()
    {
        string input = "43 2a a4 2b aa";
        InputValidator.CheckIfHex(input);
    }

    [TestMethod]
    public void TestHexInputHexSpacesMultiple()
    {
        string input = "43  2a      a4  2b   aa";
        InputValidator.CheckIfHex(input);
    }

    [TestMethod]
    public void TestHexInputEmpty()
    {
        string input = "";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }

    [TestMethod]
    public void TestHexInputSpaceOnly()
    {
        string input = " ";
        Assert.ThrowsException<FormatException>(() => InputValidator.CheckIfUint(input));

    }
}
