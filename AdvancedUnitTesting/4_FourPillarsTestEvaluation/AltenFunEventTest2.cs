using System.Runtime.CompilerServices;

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

        string sourceDir = GetCurrentSourceDirectory();
        string participantsFilePath = Path.Combine(sourceDir, "AltenFunEventTest2Participants.txt");
        string expectedReportFilePath = Path.Combine(sourceDir, "AltenFunEventTest2ExpectedReport.txt");

        // Read participants from file
        var participants = File.ReadAllLines(participantsFilePath);
        foreach (var participant in participants)
        {
            sut.RequestToJoin(participant);
        }

        // Act
        string report = sut.ReportParticipants();

        // Assert
        var expectedReport = File.ReadAllText(expectedReportFilePath);
        Assert.That(report, Is.EqualTo(expectedReport));
    }

    // A helper method that captures the file path of whatever calls it
    private static string GetCurrentSourceDirectory([CallerFilePath] string sourceFilePath = "")
    {
        return Path.GetDirectoryName(sourceFilePath);
    }
}
