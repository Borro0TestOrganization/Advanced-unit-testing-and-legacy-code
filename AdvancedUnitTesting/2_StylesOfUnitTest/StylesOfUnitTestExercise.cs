namespace AdvancedUnitTesting.StylesOfUnitTest;

public class AltenFunEvent
{
    int _maximumNumberOfParticipants;
    List<string> _participants;
    EmailService _emailService;

    public AltenFunEvent(int maximumNumberOfParticipants)
    {
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _emailService = new EmailService();
        _participants = new List<string>();
    }

    public int GetNumberOfParticipants()
    {
        return _participants.Count;
    }

    public bool MaySignUp()
    {
        return GetNumberOfParticipants() < _maximumNumberOfParticipants;
    }

    public bool DoSignUp(string email)
    {
        if (!MaySignUp())
        {
            return false;
        }

        _participants.Add(email);
        return true;
    }

    public void SendEmailToParticipants(string text)
    {
        foreach (string participant in _participants)
        {
            _emailService.SendEmail(participant, text);
        }
    }
}

public class EmailService
{
    public void SendEmail(string targetEmailAddress, string text)
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));

        Random random = new Random();
        if (random.Next(100) < 10)
        {
            // Send of actual e-mail might fail 10% of the time
            throw new TimeoutException();
        }
    }
}

public class StylesOfUnitTestExercise
{
    [Test]
    public void OUTPUT_BASED_An_employee_may_sign_up_to_an_event_that_is_not_full()
    {
        // Write an output based unit test (with AAA pattern)
    }

    [Test]
    public void STATE_BASED_The_number_of_participants_increases_by_1_when_an_employee_successfully_signed_up()
    {
        // Write state-based unit test (with AAA pattern)
    }

    [Test]
    public void COMMUNICATION_BASED_An_email_can_be_sent_to_all_participants()
    {
        // How would you write a communication based test?
        // Think about it, we will come back to it later when discussing `isolation`.
    }
}

