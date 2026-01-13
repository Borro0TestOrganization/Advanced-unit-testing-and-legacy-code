namespace AdvancedUnitTesting.LondonVsClassicalSchoolExercise;

public class ClassicalSchoolTests
{
    [Test]
    public void People_will_be_signed_up_for_the_event_first_come_first_serve()
    {
        // Arrange
        var maxAmountOfParticipants = 4;
        var altenFunEvent = new AltenFunEvent(maxAmountOfParticipants);
        var sut = new FunEventHost(altenFunEvent);

        sut.RequestToJoin("Alice");     // Consultant
        sut.RequestToJoin("Peter");     // Consultant
        sut.RequestToJoin("Bob");       // Consultant
        sut.RequestToJoin("Sandra");    // Consultant
        sut.RequestToJoin("John");      // Consultant

        // Act
        string report = sut.ReportParticipants();

        // Assert
        Assert.That(report, Is.EqualTo(
            """
            Team 1:
            Alice
            Peter
            Team 2:
            Bob
            Sandra
            """
        ));
    }

    [Test]
    public void Only_one_business_manager_per_team()
    {
        // Write another unit test in classical school tests, which verifies that if multiple business managers 
        // want to join event (3 or more), only 2 are accepted, and they are evenly spread between the 2 teams
        // The names of the known business managers are: "Hanneke", "Willem", and "Thea".
    }
}
