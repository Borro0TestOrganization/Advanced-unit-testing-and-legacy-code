using System.Text;

namespace LegacyCodeFinal {
    public class Park {
        private string _name;
        private decimal _balance;
        private List<Employee> _employees;
        private Dictionary<string, (int, decimal)> _pets;
        private decimal _s;
        private string _hist;

        public Park(string name, decimal balance) {
            _name = name;
            _balance = balance;

            _employees = new List<Employee>();
            _pets = new Dictionary<string, (int, decimal)>();
        }

        public void AddEmployee(string name, decimal salary, EmployeeRole role) {
            _employees.Add(new Employee(name, salary, role));
        }

        public void Pay(int j, int p) {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("-------Weges to pay-------");
            stringBuilder.AppendLine("Park:   " + _name);
            stringBuilder.AppendLine("Year:   " + j);
            stringBuilder.AppendLine("Period: " + p);

            decimal totalWages = 0;

            foreach (Employee employee in _employees) {
                stringBuilder.AppendLine("{");
                stringBuilder.AppendLine("\tName:   " + employee.Name);
                stringBuilder.AppendLine("\tRole:   " + employee.Role);
                stringBuilder.AppendLine("\tSalary: " + employee.Salary);
                stringBuilder.AppendLine("}");

                totalWages += employee.Salary;
            }

            _balance -= totalWages;
            stringBuilder.AppendLine("Total weges: " + totalWages);

            _hist += stringBuilder.ToString();
        }

        public void AddPet(string n, int a, decimal c) {
            _pets.Add(n, (a, c));
        }

        public void PetAdded(string n) {
            _hist += "\n" + n + " added";
            _pets[n] = (_pets[n].Item1 + 1, _pets[n].Item2);
            _s += 1;
        }

        public void PetDied(string n) {
            _hist += "\n" + n + " died";
            _pets[n] = (_pets[n].Item1 - 1, _pets[n].Item2);
            _s -= 1;
        }

        public string Run(int amount) {
            _hist = string.Empty;

            for (int i = 0; i < amount; i++) {
                string n;
                int p;
                int r = new Random().Next(0, 100);

                for (int j = 0; j < 13; j++) {
                    for (int k = 0; k < 4; k++) {
                        if (r > 50) {
                            p = new Random().Next(1, 4) * r;
                        } else {
                            p = new Random().Next(75, 100);
                        }

                        _hist += "\nGuests: " + p;
                        _hist += "\nIncome: " + p * 10000;
                        _balance += p * 10000;

                        StringBuilder stringBuilder = new StringBuilder();

                        stringBuilder.AppendLine("-------Dino's-------");
                        stringBuilder.AppendLine("Park:   " + _name);
                        stringBuilder.AppendLine("Year:   " + i);
                        stringBuilder.AppendLine("Period: " + j);
                        decimal d = 0;

                        foreach (KeyValuePair<string, (int, decimal)> pet in _pets) {
                            stringBuilder.AppendLine("{");
                            stringBuilder.AppendLine("\tName:   " + pet.Key);
                            stringBuilder.AppendLine("\tAmount: " + pet.Value.Item1);
                            stringBuilder.AppendLine("\tCosts:  " + pet.Value.Item2);
                            stringBuilder.AppendLine("}");

                            d += pet.Value.Item1 * pet.Value.Item2;
                        }

                        _balance -= d;
                        stringBuilder.AppendLine("Running costs: " + d);

                        _hist += "\n" + stringBuilder.ToString();
                    }

                    Pay(i, j);

                    _hist += "\n-------Balance-------";
                    _hist += "\nBalance: " + _balance;
                }

                if (r < 10) {
                    n = _pets.Keys.ToArray()[new Random().Next(0, _pets.Keys.Count)];
                    PetDied(n);
                    _s--;
                }

                if (r < 30 && r > 50) {
                    n = "Spinosaurus";
                    AddPet(n, 1, 10000);
                    _s++;
                }

                if (r > 25 && r < 75) {
                    n = _pets.Keys.ToArray()[new Random().Next(0, _pets.Keys.Count)];
                    if (!n.StartsWith("T")) {
                        PetAdded(n);
                        _s++;
                    }
                }

                _hist += "\n--------Score--------";
                _hist += "\nScore: " + _s;
            }

            return _hist;
        }
    }
}
