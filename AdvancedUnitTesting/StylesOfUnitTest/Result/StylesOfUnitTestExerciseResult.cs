namespace AdvancedUnitTesting.StylesOfUnitTest.Result;

public class StylesOfUnitTestExerciseResult
{
    [Test]
    public void An_employee_may_sign_up_to_an_event_that_is_not_full()
    {
        // Arrange
        AltenFunEvent altenFunEvent = new AltenFunEvent(10);

        // Act
        bool result = altenFunEvent.MaySignUp();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void The_number_of_participants_increments_when_an_employee_successfully_signed_up()
    {
        // Arrange
        AltenFunEvent altenFunEvent = new AltenFunEvent(10);

        // Act
        altenFunEvent.DoSignUp("myemail@alten.nl");

        // Assert
        Assert.That(altenFunEvent.GetNumberOfParticipants(), Is.EqualTo(1));
    }

    [Test]
    public void An_email_can_be_sent_to_all_participants()
    {
        // How would you write a communication based test?
        // Think about it, we will come back to it later when discussing `isolation`.
    }
}

