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
        // Arrange
        var altenFunEvent = Substitute.For<IAltenFunEvent>();
        var sut = new FunEventHost(altenFunEvent);

        altenFunEvent.DoSignUp(new Employee("Hanneke", EmployeeType.BusinessManager)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Hanneke").Returns(1);
        altenFunEvent.DoSignUp(new Employee("Willem", EmployeeType.BusinessManager)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Willem").Returns(2);
        altenFunEvent.DoSignUp(new Employee("Bob", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("Bob").Returns(1);
        altenFunEvent.DoSignUp(new Employee("Thea", EmployeeType.BusinessManager)).Returns(false);
        altenFunEvent.DoSignUp(new Employee("John", EmployeeType.Consultant)).Returns(true);
        altenFunEvent.GetTeamForParticipant("John").Returns(2);

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
