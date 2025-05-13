namespace UnitTest1Exercise;

public class Calculator
{
    public double Sum(double a, double b)
    {
        return a + b;
    }
}

// How to write assertions with NUnit:
// Assert.That(actual, Is.EqualTo(expected));        // Equality
// Assert.That(value, Is.Not.Null);                  // Not null
// Assert.That(list, Has.Count.EqualTo(3));          // Collection count
// Assert.That(str, Does.Contain("test"));           // String contains
// Assert.That(number, Is.InRange(1, 10));           // Value in range

public class Simple_AAA_pattern
{
    [Test]
    public void The_sum_of_two_numbers_can_be_calculated()
    {
        // Create  test with the Arrange / Act / Assert pattern here...
    }
}
