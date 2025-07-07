namespace AdvancedUnitTesting.AAAPattern.MultipleActAssert;

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

public class AvoidMultipleArrangeActAssertSections
{
    [Test]
    public void Calculator_supports_basic_operations()
    {
        // Refactor this test into multiple tests with single arrange / act / assert sections
        double first = 10;
        double second = 20;
        var sut = new Calculator();

        var result = sut.Sum(first, second);
        Assert.That(result, Is.EqualTo(30));

        result = sut.Multiply(first, second);
        Assert.That(result, Is.EqualTo(200));

        result = sut.Divide(first, second);
        Assert.That(result, Is.EqualTo(0.5));
    }
}
