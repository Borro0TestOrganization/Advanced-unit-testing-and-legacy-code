using LegacyCodeFinalResult;

namespace LegacyCodeFinalResultTest
{
    public class EmployeeRaiseTest
    {
        [Test]
        public void A_non_it_guy_employee_gets_a_raise_based_on_the_park_score()
        {
            // Arrange
            Employee employee = new Employee("John", 1000, EmployeeRole.Doctor);
            List<Employee> employees = new List<Employee> { employee };
            decimal score = 3;

            // Act
            Park.GiveEmployeesRaise(employees, score);

            // Assert
            Assert.That(employee.Salary, Is.EqualTo(1030));
        }

        [Test]
        public void The_it_guy_will_never_get_a_raise()
        {
            // Arrange
            Employee employee = new Employee("John", 1000, EmployeeRole.IT);
            List<Employee> employees = new List<Employee> { employee };
            decimal score = 3;

            // Act
            Park.GiveEmployeesRaise(employees, score);

            // Assert
            Assert.That(employee.Salary, Is.EqualTo(1000));
        }

    }
}
