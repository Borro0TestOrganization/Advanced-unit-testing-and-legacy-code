namespace AdvancedUnitTesting.FourPillarsTestEvaluation;

// Looking at the four pillars, is there a pillar which gets violated in this test?
public class Test1
{
    [Test]
    public void Maximum_number_of_participants_are_evenly_distributed_per_team()
    {
        // Arrange
        int maximumNumberOfParticipants = 10;
        var sut = new AltenFunEvent(maximumNumberOfParticipants);

        // Act
        int numberOfParticipantsTeam1 = sut._maxTeamMembersTeam1;
        int numberOfParticipantsTeam2 = sut._maxTeamMembersTeam2;

        // Assert
        Assert.That(numberOfParticipantsTeam1, Is.EqualTo(5));
        Assert.That(numberOfParticipantsTeam2, Is.EqualTo(5));
    }
}
