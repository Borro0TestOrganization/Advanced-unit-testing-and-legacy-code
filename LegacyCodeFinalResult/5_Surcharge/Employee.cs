namespace LegacyCodeFinalResult._5_Surcharge {
    internal class Employee {
        private static int NEXT_ID = 1;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Salary { get; private set; }
        public EmployeeRole Role { get; private set; }

        public Employee(string name, decimal salary, EmployeeRole role) {
            Id = NEXT_ID;
            NEXT_ID++;

            Name = name;
            Salary = salary;
            Role = role;
        }

        public void SalaryIncrease(decimal salaryIncrease) {
            Salary += salaryIncrease;
        }
    }
}
