namespace AdvancedUnitTesting.LondonVsClassicalSchoolExercise;

using NSubstitute;

public class LondonSchoolTests
{
    [Test]
    public void People_will_be_signed_up_for_the_event_first_come_first_serve()
    {
        // Arrange
        var altenFunEvent = Substitute.For<IAltenFunEvent>();
        var sut = new FunEventHost(altenFunEvent);

        altenFunEvent.DoSignUp(new Employee("Alice", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Alice").Returns(1);
        altenFunEvent.DoSignUp(new Employee("Peter", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Peter").Returns(1);
        altenFunEvent.DoSignUp(new Employee("Bob", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Bob").Returns(2);
        altenFunEvent.DoSignUp(new Employee("Sandra", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Sandra").Returns(2);
        altenFunEvent.DoSignUp(new Employee("John", EmployeeType.Consultant)).Returns(false);

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
        // Write another unit test in london school test style, which verifies that if multiple business managers 
        // want to join event (3 or more), only 2 are accepted, and they are evenly spread between the 2 teams
        // The names of the known business managers are: "Hanneke", "Willem", and "Thea".
    }
}
