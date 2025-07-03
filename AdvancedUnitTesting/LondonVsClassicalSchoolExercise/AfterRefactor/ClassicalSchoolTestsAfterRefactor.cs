namespace AdvancedUnitTesting.LondonVsClassicalSchoolExercise.AfterRefactor;

public class ClassicalSchoolTestsAfterRefactor
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
        // Arrange
        var maxAmountOfParticipants = 4;
        var altenFunEvent = new AltenFunEvent(maxAmountOfParticipants);
        var sut = new FunEventHost(altenFunEvent);

        sut.RequestToJoin("Hanneke");   // Business Manager
        sut.RequestToJoin("Willem");    // Business Manager
        sut.RequestToJoin("Bob");       // Consultant
        sut.RequestToJoin("Thea");      // Business Manager
        sut.RequestToJoin("John");      // Consultant

        // Act
        string report = sut.ReportParticipants();

        // Assert
        Assert.That(report, Is.EqualTo(
            """
            Team 1:
            Hanneke
            Bob
            Team 2:
            Willem
            John
            """
            ));
    }
}
