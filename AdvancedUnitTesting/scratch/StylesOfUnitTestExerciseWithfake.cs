namespace AdvancedUnitTesting.scratch;

public class AltenFunEvent
{
    int _maximumNumberOfParticipants;
    List<string> _participants;
    IEmailService _emailService;

    public AltenFunEvent(int maximumNumberOfParticipants, IEmailService emailService)
    {
        _maximumNumberOfParticipants = maximumNumberOfParticipants;
        _emailService = emailService;
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

public interface IEmailService
{
    public void SendEmail(string targetEmailAddress, string text);
}

public class EmailService : IEmailService
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

public class FakeEmailService : IEmailService
{
    public List<string> EmailsSentTo = [];

    public void SendEmail(string targetEmailAddress, string text)
    {
        EmailsSentTo.Add(targetEmailAddress);
    }
}


public class StylesOfUnitTestExercise
{
    [Test]
    public void An_employee_may_sign_up_to_an_event_that_is_not_full()
    {
        // Write an output based unit test (with AAA pattern)
    }

    [Test]
    public void The_number_of_participants_increments_when_an_employee_successfully_signed_up()
    {
        // Write state-based unit test (with AAA pattern)
    }

    [Test]
    public void An_email_can_be_sent_to_all_participants()
    {
        // Write communication based test (with AAA pattern)
    }
}

