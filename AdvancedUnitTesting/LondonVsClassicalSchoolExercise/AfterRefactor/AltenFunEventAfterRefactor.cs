namespace AdvancedUnitTesting.LondonVsClassicalSchoolExercise.AfterRefactor;

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
    string ReportParticipants();
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

    public string ReportParticipants()
    {
        string result = "";
        result += "Team 1:" + Environment.NewLine;
        foreach (Employee employee in _team1)
        {
            result += employee.Name + Environment.NewLine;
        }
        result += "Team 2:" + Environment.NewLine;
        foreach (Employee employee in _team2)
        {
            result += employee.Name + Environment.NewLine;
        }

        return result.Trim();
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

    public FunEventHost(IAltenFunEvent altenFunEvent)
    {
        _altenFunEvent = altenFunEvent;
        _employeeDatabase = new EmployeeDatabase();
    }

    public void RequestToJoin(string name)
    {
        var employee = _employeeDatabase.FindEmployeeByName(name);
        _altenFunEvent.DoSignUp(employee);
    }

    public string ReportParticipants()
    {
        return _altenFunEvent.ReportParticipants();
    }
}
