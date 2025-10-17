using System.Text;

namespace LegacyCodeFinalResult._3_Random {
    public class Park {
        private string _name;
        private decimal _balance;
        private List<Employee> _employees;
        private Dictionary<string, (int amount, decimal cost)> _dinosaurs;
        private decimal _score;
        private string _historyLog;
        private ParkCycleBalance _parkCycleBalance;

        public Park(string name, decimal balance) {
            _name = name;
            _balance = balance;

            _employees = new List<Employee>();
            _dinosaurs = new Dictionary<string, (int, decimal)>();

            _parkCycleBalance = new ParkCycleBalance();
        }

        public void AddEmployee(string name, decimal salary, EmployeeRole role) {
            _employees.Add(new Employee(name, salary, role));
        }

        public void Pay(int year, int period) {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("-------Weges to pay-------");
            stringBuilder.AppendLine("Park:   " + _name);
            stringBuilder.AppendLine("Year:   " + year);
            stringBuilder.AppendLine("Period: " + period);

            decimal totalWages = 0;

            foreach (Employee employee in _employees) {
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("\tName:   " + employee.Name);
                stringBuilder.AppendLine("\tRole:   " + employee.Role);
                stringBuilder.AppendLine("\tSalary: " + employee.Salary);
                stringBuilder.AppendLine("}");

                totalWages += employee.Salary;
            }

            _parkCycleBalance.AddDebit(totalWages);
            stringBuilder.AppendLine("Total weges: " + totalWages);

            _historyLog += stringBuilder.ToString();
        }

        public void AddDinosaur(string name, int amount, decimal cost) {
            _dinosaurs.Add(name, (amount, cost));
        }

        public void DinosaurAdded(string name) {
            _historyLog += "\n" + name + " added";
            _dinosaurs[name] = (_dinosaurs[name].amount + 1, _dinosaurs[name].cost);
            _score += 1;
        }

        public void DinosaurDied(string name) {
            _historyLog += "\n" + name + " died";
            _dinosaurs[name] = (_dinosaurs[name].amount - 1, _dinosaurs[name].cost);
            _score -= 1;
        }

        public string Run(int amount) {
            _historyLog = string.Empty;

            for (int year = 0; year < amount; year++) {
                string dinosaurName;
                int amountOfGuests;
                int randomValue = new Random().Next(0, 100);

                for (int period = 0; period < 13; period++) {
                    for (int week = 0; week < 4; week++) {
                        if (randomValue > 50) {
                            amountOfGuests = new Random().Next(1, 4) * randomValue;
                        } else {
                            amountOfGuests = new Random().Next(75, 100);
                        }

                        _historyLog += "\nGuests: " + amountOfGuests;

                        decimal incomeFromGuests = amountOfGuests * 1000;
                        _parkCycleBalance.AddCredit(incomeFromGuests);
                        _historyLog += "\nIncome: " + incomeFromGuests;

                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.AppendLine("-------Dino's-------");
                        stringBuilder.AppendLine("Park:   " + _name);
                        stringBuilder.AppendLine("Year:   " + year);
                        stringBuilder.AppendLine("Period: " + period);
                        decimal runningCosts = 0;

                        foreach (KeyValuePair<string, (int amount, decimal cost)> dinosaur in _dinosaurs) {
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("\tName:   " + dinosaur.Key);
                            stringBuilder.AppendLine("\tAmount: " + dinosaur.Value.amount);
                            stringBuilder.AppendLine("\tCosts:  " + dinosaur.Value.cost);
                            stringBuilder.AppendLine("}");

                            runningCosts += dinosaur.Value.amount * dinosaur.Value.cost;
                        }

                        _parkCycleBalance.AddDebit(runningCosts);
                        stringBuilder.AppendLine("Running costs: " + runningCosts);

                        _historyLog += "\n" + stringBuilder.ToString();
                    }

                    Pay(year, period);

                    _historyLog += _parkCycleBalance.Print();
                    _balance += _parkCycleBalance.GetBalance();
                    _parkCycleBalance.Reset();

                    _historyLog += "\n-------Balance-------";
                    _historyLog += "\nBalance: " + _balance;
                }

                if (randomValue < 10) {
                    dinosaurName = _dinosaurs.Keys.ToArray()[new Random().Next(0, _dinosaurs.Keys.Count)];
                    DinosaurDied(dinosaurName);
                    _score--;
                }

                if (randomValue < 30 && randomValue > 50) {
                    dinosaurName = "Spinosaurus";
                    AddDinosaur(dinosaurName, 1, 10000);
                    _score++;
                }

                if (randomValue > 25 && randomValue < 75) {
                    dinosaurName = _dinosaurs.Keys.ToArray()[new Random().Next(0, _dinosaurs.Keys.Count)];
                    if (!dinosaurName.StartsWith("T")) {
                        DinosaurAdded(dinosaurName);
                        _score++;
                    }
                }

                _historyLog += "\n--------Score--------";
                _historyLog += "\nScore: " + _score;
            }

            return _historyLog;
        }
    }
}
