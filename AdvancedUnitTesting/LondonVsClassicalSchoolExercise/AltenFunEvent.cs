


using NSubstitute;

namespace AdvancedUnitTesting.LondonVsClassicalSchoolExercise;

public record Employee
{
    public Employee(string name, EmployeeType type)
    {
        Name = name;
        Type = type;
    }
    public string Name { get; }
    public EmployeeType Type { get; }
}

public interface IAltenFunEvent
{
    bool DoSignUp(Employee employee);
    int GetTeamForParticipant(string participant);
}

public class AltenFunEvent : IAltenFunEvent
{
    int _maxTeamMembersTeam1;
    int _maxTeamMembersTeam2;
    List<Employee> _team1;
    List<Employee> _team2;

    public AltenFunEvent(int maximumNumberOfParticipants)
    {
        _maxTeamMembersTeam1 = maximumNumberOfParticipants - maximumNumberOfParticipants / 2;
        _maxTeamMembersTeam2 = maximumNumberOfParticipants / 2;
        _team1 = [];
        _team2 = [];
    }

    public bool DoSignUp(Employee employee)
    {
        if (employee.Type == EmployeeType.Consultant)
        {
            return AssignConsultantToTeam(employee);
        }

        if (employee.Type == EmployeeType.BusinessManager)
        {
            return AssignBusinessManagerToTeam(employee);
        }

        return false;
    }

    private bool AssignBusinessManagerToTeam(Employee employee)
    {
        bool result = false;

        if (_team1.Count < _maxTeamMembersTeam1 && _team1.All(employee => employee.Type == EmployeeType.Consultant))
        {
            _team1.Add(employee);
            result = true;
        }
        else if (_team2.Count < _maxTeamMembersTeam2 && _team2.All(employee => employee.Type == EmployeeType.Consultant))
        {
            _team2.Add(employee);
            result = true;
        }

        return result;
    }

    private bool AssignConsultantToTeam(Employee employee)
    {
        bool result = false;

        if (_team1.Count < _maxTeamMembersTeam1)
        {
            _team1.Add(employee);
            result = true;
        }
        else if (_team2.Count < _maxTeamMembersTeam2)
        {
            _team2.Add(employee);
            result = true;
        }

        return result;
    }

    public int GetTeamForParticipant(string participant)
    {
        if (_team1.Any(employee => employee.Name == participant))
        {
            return 1;
        }

        if (_team2.Any(employee => employee.Name == participant))
        {
            return 2;
        }

        return -1;
    }
}

public enum EmployeeType
{
    Consultant,
    BusinessManager
}

public class EmployeeDatabase
{
    private readonly List<Employee> _allEmployees = [
            new("Alice", EmployeeType.Consultant),
            new("Peter", EmployeeType.Consultant),
            new("Bob", EmployeeType.Consultant),
            new("John", EmployeeType.Consultant),
            new("Sandra", EmployeeType.Consultant),

            new("Hanneke", EmployeeType.BusinessManager),
            new("Willem", EmployeeType.BusinessManager),
            new("Thea", EmployeeType.BusinessManager),
        ];

    public Employee FindEmployeeByName(string name)
    {
        return _allEmployees.First(employee => employee.Name == name);
    }
}

public class FunEventHost
{
    private readonly IAltenFunEvent _altenFunEvent;
    private readonly EmployeeDatabase _employeeDatabase;
    List<string> _participants;

    public FunEventHost(IAltenFunEvent altenFunEvent)
    {
        _altenFunEvent = altenFunEvent;
        _employeeDatabase = new EmployeeDatabase();
        _participants = [];
    }

    public void RequestToJoin(string name)
    {
        var employee = _employeeDatabase.FindEmployeeByName(name);
        bool isSignedUp = _altenFunEvent.DoSignUp(employee);
        if (isSignedUp)
        {
            _participants.Add(name);
        }
    }

    public string ReportParticipants()
    {
        string result = "";
        result += "Team 1:" + Environment.NewLine;
        foreach (string participant in _participants)
        {
            int team = _altenFunEvent.GetTeamForParticipant(participant);
            if (team == 1)
            {
                result += participant + Environment.NewLine;
            }
        }
        result += "Team 2:" + Environment.NewLine;
        foreach (string participant in _participants)
        {
            int team = _altenFunEvent.GetTeamForParticipant(participant);
            if (team == 2)
            {
                result += participant + Environment.NewLine;
            }
        }

        return result.Trim();
    }
}

public class ClassicalSchoolTests
{
    [Test]
    public void People_will_be_signed_up_for_the_event_first_come_first_serve()
    {
        // Arrange
        var altenFunEvent = new AltenFunEvent(4);
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
        var altenFunEvent = new AltenFunEvent(4);
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
