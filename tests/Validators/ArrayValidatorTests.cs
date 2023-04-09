namespace Panbyte.tests.Validators;
using Panbyte.Validators;

[TestClass]
public class ArrayValidatorTests
{
    [TestMethod]
    public void TestIsHexValid()
    {
        string input = "0xaf";
        Assert.IsTrue(ArrayValidator.IsHex(input));
    }

    [TestMethod]
    public void TestIsHexInvalidChar()
    {
        string input = "0xqq";
        Assert.IsFalse(ArrayValidator.IsHex(input));
    }

    [TestMethod]
    public void TestIsHexOdd()
    {
        string input = "0xa";
        Assert.IsFalse(ArrayValidator.IsHex(input));
    }

    [TestMethod]
    public void TestIsDecimalValid()
    {
        string input = "125";
        Assert.IsTrue(ArrayValidator.IsDecimal(input));
    }

    [TestMethod]
    public void TestIsDecimalInvalidChar()
    {
        string input = "125ab21";
        Assert.IsFalse(ArrayValidator.IsDecimal(input));
    }

    [TestMethod]
    public void TestIsDecimalNegative()
    {
        string input = "-125";
        Assert.IsFalse(ArrayValidator.IsDecimal(input));
    }

    [TestMethod]
    public void TestIsCharValid()
    {
        string input = "'{'";
        Assert.IsTrue(ArrayValidator.IsChar(input));
    }

    [TestMethod]
    public void TestIsCharNonAscii()
    {
        string input = "'€'";
        Assert.IsFalse(ArrayValidator.IsChar(input));
    }

    [TestMethod]
    public void TestIsCharMultiple()
    {
        string input = "'abc'";
        Assert.IsFalse(ArrayValidator.IsChar(input));
    }

    [TestMethod]
    public void TestIsCharHexValid()
    {
        string input = @"'\x04'";
        Assert.IsTrue(ArrayValidator.IsCharHex(input));
    }

    [TestMethod]
    public void TestIsCharHexInvalidChar()
    {
        string input = @"'\x0q'";
        Assert.IsFalse(ArrayValidator.IsCharHex(input));
    }

    [TestMethod]
    public void TestIsCharHexMissingApostrophes()
    {
        string input = @"\x04";
        Assert.IsFalse(ArrayValidator.IsCharHex(input));
    }

    [TestMethod]
    public void TestIsBitsValid()
    {
        string input = "0b1010";
        Assert.IsTrue(ArrayValidator.IsBits(input));
    }

    [TestMethod]
    public void TestIsBitsInvalidChar()
    {
        string input = "0b101q";
        Assert.IsFalse(ArrayValidator.IsBits(input));
    }

    [TestMethod]
    public void TestFormatValidationValid1()
    {
        string input = "{1, {2}, {3}}";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationValid2()
    {
        string input = "[[1, 2], [3, 4], [5, 6]]";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationValid3()
    {
        string input = "{{0x01, (2), [3, 0b100, 0x05], '\x06'}}";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationValidBracket()
    {
        string input = "{1, '}', 3}";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationValidComma()
    {
        string input = "{1, ',', 3}";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationValidEmpty()
    {
        string input = "([],{})";
        ArrayValidator.CheckValidPosition(input);
    }

    [TestMethod]
    public void TestFormatValidationOutsideBracketLeft()
    {
        string input = "{1, 2, 3 {}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationOutsideBracketRight()
    {
        string input = "{1, 2, {} 3}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationBetweenBracketComma()
    {
        string input = "{1, {2}15, {3}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationMissingComma()
    {
        string input = "{1, {2} {3}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationMissingCommaDeep()
    {
        string input = "{{1, 2}, {3, {4} {5}}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationCharAfter()
    {
        string input = "{1, {2}, {3}}w";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationCharBefore()
    {
        string input = "a{1, {2}, {3}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }

    [TestMethod]
    public void TestFormatValidationCommaApostrophes()
    {
        string input = "{1 ',' {2}, {3}}";
        Assert.ThrowsException<FormatException>(() => ArrayValidator.CheckValidPosition(input));
    }
}