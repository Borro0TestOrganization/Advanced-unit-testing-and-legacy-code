namespace AdvancedUnitTesting.FourPillarsTestEvaluation;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test2
{
    [Test]
    public void People_will_be_signed_up_for_the_event_first_come_first_serve()
    {
        // Arrange
        var maxAmountOfParticipants = 4;
        var altenFunEvent = new AltenFunEvent(maxAmountOfParticipants);
        var sut = new FunEventHost(altenFunEvent);

        // Read participants from file
        var participants = File.ReadAllLines("..\\..\\..\\..\\AdvancedUnitTesting\\4_FourPillarsTestEvaluation\\AltenFunEventTest2Participants.txt");
        foreach (var participant in participants)
        {
            sut.RequestToJoin(participant);
        }

        // Act
        string report = sut.ReportParticipants();

        // Assert
        var expectedReport = File.ReadAllText("..\\..\\..\\..\\AdvancedUnitTesting\\4_FourPillarsTestEvaluation\\AltenFunEventTest2ExpectedReport.txt");
        Assert.That(report, Is.EqualTo(expectedReport));
    }
}
