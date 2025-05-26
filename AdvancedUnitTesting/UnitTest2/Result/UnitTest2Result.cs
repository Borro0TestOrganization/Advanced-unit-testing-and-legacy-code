namespace AdvancedUnitTesting.UnitTest2.Result;

public class Calculator
{
    public double Sum(double a, double b)
    {
        return a + b;
    }

    public double Multiply(double a, double b)
    {
        return a * b;
    }

    public double Divide(double a, double b)
    {
        return a / b;
    }
}

public class UnitTest2Result
{
    [Test]
    public void Test_sum_of_two_numbers()
    {
        // Arrange
        double first = 10;
        double second = 20;
        var sut = new Calculator();

        // Act
        var result = sut.Sum(first, second);

        // Assert
        Assert.That(result, Is.EqualTo(30));
    }

    [Test]
    public void Test_multiplying_two_numbers()
    {
        // Arrange
        double first = 10;
        double second = 20;
        var sut = new Calculator();

        // Act
        var result = sut.Multiply(first, second);

        // Assert
        Assert.That(result, Is.EqualTo(200));
    }

    [Test]
    public void Test_dividing_two_numbers()
    {
        // Arrange
        double first = 10;
        double second = 20;
        var sut = new Calculator();

        // Act
        var result = sut.Divide(first, second);

        // Assert
        Assert.That(result, Is.EqualTo(0.5));
    }
}
