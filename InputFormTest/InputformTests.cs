namespace Wahrscheinlichkeiten.UnitTests;

[TestClass]
public class InputformTests
{
    const int number = -1;
    
    #region 2 <> X
    public static void TestMethodBase(string input, InputForm expected)
    {
        //Act
        var output = Input.GetInput(input);

        //Assert
        Assert.AreEqual(expected, output);
    }
    
    [TestMethod]
    public void TestMethod1()
    {
        //Arrange
        string input = number + " < X";
        InputForm expected = new('X', Sign.smallerThan, number, Sign.none, null);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod2()
    {
        //Arrange
        string input = number + " > X";
        InputForm expected = new('X', Sign.greaterThan, number, Sign.none, null);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod3()
    {
        //Arrange
        string input = number + ">=X";
        InputForm expected = new('X', Sign.greaterThanOrEqualTo, number, Sign.none, null);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod4()
    {
        //Arrange
        string input = number + "<=X";
        InputForm expected = new('X', Sign.smallerThanOrEqualTo, number, Sign.none, null);

        TestMethodBase(input, expected);
    }
    #endregion

    #region X <> 2
    [TestMethod]
    public void TestMethod5()
    {
        //Arange
        string input = "X > " + number;

        void Act() => Input.GetInput(input);

        //Act & Assert
        Assert.ThrowsException<FormatException>(Act);
    }

    [TestMethod]
    public void TestMethod6()
    {
        //Arange
        string input = "X < " + number;

        void Act() => Input.GetInput(input);

        //Act & Assert
        Assert.ThrowsException<FormatException>(Act);
    }
    #endregion

    #region 2 <> X <> 2
    const int secondNumber = 2;
    const char var = 'A';

    [TestMethod]
    public void TestMethod7()
    {
        //Arrange
        string input = $"{number} < {var} < {secondNumber}";
        InputForm expected = new(var, Sign.smallerThan, number, Sign.smallerThan, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod8()
    {
        //Arrange
        string input = $"{number} > {var} < {secondNumber}";
        InputForm expected = new(var, Sign.greaterThan, number, Sign.smallerThan, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod9()
    {
        //Arrange
        string input = $"{number} >= {var} < {secondNumber}";
        InputForm expected = new(var, Sign.greaterThanOrEqualTo, number, Sign.smallerThan, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod10()
    {
        //Arrange
        string input = $"{number} <= {var} < {secondNumber}";
        InputForm expected = new(var, Sign.smallerThanOrEqualTo, number, Sign.smallerThan, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod11()
    {
        //Arrange
        string input = $"{number} < {var} > {secondNumber}";
        InputForm expected = new(var, Sign.smallerThan, number, Sign.greaterThan, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod12()
    {
        //Arrange
        string input = $"{number} < {var} <= {secondNumber}";
        InputForm expected = new(var, Sign.smallerThan, number, Sign.smallerThanOrEqualTo, secondNumber);

        TestMethodBase(input, expected);
    }

    [TestMethod]
    public void TestMethod13()
    {
        //Arrange
        string input = $"{number} < {var} >= {secondNumber}";
        InputForm expected = new(var, Sign.smallerThan, number, Sign.greaterThanOrEqualTo, secondNumber);

        TestMethodBase(input, expected);
    }

    #endregion
}