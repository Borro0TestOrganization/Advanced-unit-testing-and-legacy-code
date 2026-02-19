// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.

namespace AdvancedUnitTesting.StylesOfUnitTest.SlideExample;

public class OutputBasedCalculator
{
    public static int Sum(string numbers)
    {
        string[] splitNumbers = numbers.Split(',');

        int a = int.Parse(splitNumbers[0]);
        int b = int.Parse(splitNumbers[1]);

        return a + b;
    }
}

public class StateBasedCalculator
{
    public int TotalSoFar { get; private set; }

    public void Sum(string numbers)
    {
        string[] splitNumbers = numbers.Split(',');

        int a = int.Parse(splitNumbers[0]);
        int b = int.Parse(splitNumbers[1]);

        int result = a + b;

        TotalSoFar += result;
    }
}

public class CommunicationBasedCalculator
{
    private SumDatabase sumDatabase = new();

    public void Sum(string numbers)
    {
        string[] splitNumbers = numbers.Split(',');

        int a = int.Parse(splitNumbers[0]);
        int b = int.Parse(splitNumbers[1]);

        int result = a + b;

        sumDatabase.StoreSum(result);
    }
}

public class SumDatabase
{
    public void StoreSum(int result) { }
}

public class CalculatorTest
{
    [Test]
    public void The_sum_of_two_numbers_can_be_calculated()
    {
        // Arrange

        // Act
        int result = OutputBasedCalculator.Sum("1,2");

        // Assert
        Assert.That(result, Is.EqualTo(3));
    }


    [Test]
    public void State_based_calculator()
    {
        // Arrange
        var calculator = new StateBasedCalculator();

        // Act
        calculator.Sum("1,2");
        calculator.Sum("5,6");

        // Assert
        Assert.That(calculator.TotalSoFar, Is.EqualTo(14));
    }
}
