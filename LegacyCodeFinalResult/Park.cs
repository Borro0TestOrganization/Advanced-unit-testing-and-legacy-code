using System.Text;

namespace LegacyCodeFinalResult
{
    public class Park
    {
        private string _name;
        private decimal _balance;
        private List<Employee> _employees;
        private Dictionary<string, (int Amount, decimal Cost)> _dinosaurs;
        private decimal _score;
        private string _log;

        private IRandom _random;

        public Park(string name, decimal balance) : this(name, balance, new RealRandom())
        {
        }

        public Park(string name, decimal balance, IRandom random)
        {
            _name = name;
            _balance = balance;

            _employees = new List<Employee>();
            _dinosaurs = new Dictionary<string, (int, decimal)>();

            _random = random;
        }

        public void AddEmployee(string name, decimal salary, EmployeeRole role)
        {
            _employees.Add(new Employee(name, salary, role));
        }

        public void PayEmployees(int year, int period)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("-------Weges to pay-------");
            stringBuilder.AppendLine("Park:   " + _name);
            stringBuilder.AppendLine("Year:   " + year);
            stringBuilder.AppendLine("Period: " + period);

            decimal totalWages = 0;

            foreach (Employee employee in _employees)
            {
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("\tName:   " + employee.Name);
                stringBuilder.AppendLine("\tRole:   " + employee.Role);
                stringBuilder.AppendLine("\tSalary: " + employee.Salary);
                stringBuilder.AppendLine("}");

                totalWages += employee.Salary;
            }

            _balance -= totalWages;
            stringBuilder.AppendLine("Total weges: " + totalWages);

            _log += stringBuilder.ToString();
        }

        public void AddDinosaur(string n, int a, decimal c)
        {
            _dinosaurs.Add(n, (a, c));
        }

        public void DinosaurAdded(string n)
        {
            _log += "\n" + n + " added";
            _dinosaurs[n] = (_dinosaurs[n].Amount + 1, _dinosaurs[n].Cost);
            _score += 1;
        }

        public void DinosaurDied(string n)
        {
            _log += "\n" + n + " died";
            _dinosaurs[n] = (_dinosaurs[n].Amount - 1, _dinosaurs[n].Cost);
            _score -= 1;
        }

        public string Run(int totalNumberOfYears)
        {
            return Run(totalNumberOfYears, new RealRandom());
        }

        public string Run(int totalNumberOfYears, IRandom random)
        {
            _log = string.Empty;

            for (int year = 0; year < totalNumberOfYears; year++)
            {
                string n;
                int numberOfGuests;
                int randomBetween0and100 = random.Next(0, 100);

                for (int period = 0; period < 13; period++)
                {
                    for (int week = 0; week < 4; week++)
                    {
                        if (randomBetween0and100 > 50)
                        {
                            numberOfGuests = random.Next(1, 4) * randomBetween0and100;
                        }
                        else
                        {
                            numberOfGuests = random.Next(75, 100);
                        }

                        _log += "\nGuests: " + numberOfGuests;
                        _log += "\nIncome: " + numberOfGuests * 10000;
                        _balance += numberOfGuests * 10000;

                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.AppendLine("-------Dino's-------");
                        stringBuilder.AppendLine("Park:   " + _name);
                        stringBuilder.AppendLine("Year:   " + year);
                        stringBuilder.AppendLine("Period: " + period);
                        decimal dinosaurCosts = 0;

                        foreach (KeyValuePair<string, (int Amount, decimal Cost)> dinosaur in _dinosaurs)
                        {
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("\tName:   " + dinosaur.Key);
                            stringBuilder.AppendLine("\tAmount: " + dinosaur.Value.Amount);
                            stringBuilder.AppendLine("\tCosts:  " + dinosaur.Value.Cost);
                            stringBuilder.AppendLine("}");

                            dinosaurCosts += dinosaur.Value.Item1 * dinosaur.Value.Cost;
                        }

                        _balance -= dinosaurCosts;
                        stringBuilder.AppendLine("Running costs: " + dinosaurCosts);

                        _log += "\n" + stringBuilder.ToString();
                    }

                    PayEmployees(year, period);

                    _log += "\n-------Balance-------";
                    _log += "\nBalance: " + _balance;
                }

                if (randomBetween0and100 < 10)
                {
                    n = _dinosaurs.Keys.ToArray()[random.Next(0, _dinosaurs.Keys.Count)];
                    DinosaurDied(n);
                    _score--;
                }

                if (randomBetween0and100 > 25 && randomBetween0and100 < 75)
                {
                    n = _dinosaurs.Keys.ToArray()[random.Next(0, _dinosaurs.Keys.Count)];
                    if (!n.StartsWith("T"))
                    {
                        DinosaurAdded(n);
                        _score++;
                    }
                }

                _log += "\n--------Score--------";
                _log += "\nScore: " + _score;
            }

            return _log;
        }
    }
}
