using System.Text.RegularExpressions;

namespace LegacyCodeFinalResult._5_Surcharge {
    public class ParkTest {
        private RandomServiceSub _randomService;

        [SetUp]
        public void SetUp() {
            _randomService = new RandomServiceSub();
        }

        [Test]
        public void JurassicParkTest() {
            // Arrange
            Park jurassicPark = new Park("Jurassic Park", 50000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
            jurassicPark.AddEmployee("Henry Wu", 60000, EmployeeRole.Doctor);
            jurassicPark.AddEmployee("Dennis Nedry", 2500, EmployeeRole.IT);
            jurassicPark.AddEmployee("Robert Muldoon", 12500, EmployeeRole.Security);

            jurassicPark.AddEmployee("Donald Gennaro", 20000, EmployeeRole.Insurance);

            jurassicPark.AddEmployee("Alan Grant", 30000, EmployeeRole.Guide);
            jurassicPark.AddEmployee("Elle Sattler", 15000, EmployeeRole.Guide);
            jurassicPark.AddEmployee("Lan Malcolm", 160000, EmployeeRole.Guide);

            jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);
            jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);
            jurassicPark.AddDinosaur("Gallimimus", 120, 250);
            jurassicPark.AddDinosaur("Triceratops", 6, 1000);
            jurassicPark.AddDinosaur("Velociraptor", 4, 2500);
            jurassicPark.AddDinosaur("Dilophosaurus", 8, 1600);
            jurassicPark.AddDinosaur("Parasaurolophus", 24, 500);

            // Act
            string result = jurassicPark.Run(1);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void SiteBTest() {
            // Arrange
            Park jurassicPark = new Park("Site B", 650000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 450000, EmployeeRole.Owner);
            jurassicPark.AddEmployee("Peter Ludlow", 250000, EmployeeRole.Owner);

            jurassicPark.AddEmployee("Lan Malcolm", 50000, EmployeeRole.Guide);
            jurassicPark.AddEmployee("Sarah Harding", 50000, EmployeeRole.Guide);
            jurassicPark.AddEmployee("Nick Van Owen", 50000, EmployeeRole.Guide);
            jurassicPark.AddEmployee("Eddie Carr", 50000, EmployeeRole.Guide);

            jurassicPark.AddDinosaur("Tyrannosaurus", 3, 8000);
            jurassicPark.AddDinosaur("Mamenchisaurus", 6, 2000);
            jurassicPark.AddDinosaur("Pachycephalosaurus", 64, 500);
            jurassicPark.AddDinosaur("Parasaurolophus", 12, 1250);
            jurassicPark.AddDinosaur("Procompsognathus", 86, 250);
            jurassicPark.AddDinosaur("Pteranodon", 8, 3200);
            jurassicPark.AddDinosaur("Stegosaurus", 12, 1250);
            jurassicPark.AddDinosaur("Triceratops", 8, 1250);
            jurassicPark.AddDinosaur("Gallimimus", 120, 250);
            jurassicPark.AddDinosaur("Velociraptor", 14, 2500);

            // Act
            string result = jurassicPark.Run(1);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void ParkDebitCreditTest() {
            // Arrange
            int randomValue = 20;
            _randomService.AddValue(new Tuple<int, int>(0, 100), randomValue);
            Park jurassicPark = new Park("Jurassic Park", 50000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
            jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);

            // Act
            string result = jurassicPark.Run(1);

            // Assert
            Assert.That(Regex.Count(result, "Credit:   3000000"), Is.EqualTo(13));
            Assert.That(Regex.Count(result, "Debit :   1432000"), Is.EqualTo(13));
        }

        [Test]
        public void ParkCycleBalanceTest() {
            // Arrange
            ParkCycleBalance parkCycleBalance = new ParkCycleBalance();
            parkCycleBalance.AddDebit(1000);
            parkCycleBalance.AddCredit(2000);

            // Act
            decimal result = parkCycleBalance.GetBalance();

            // Assert
            Assert.That(result, Is.EqualTo(1000));
        }

        [Test]
        public void ParkSalaryIncreaseTest() {
            // Arrange
            int randomValue = 70;
            _randomService.AddValue(new Tuple<int, int>(0, 100), randomValue);
            Park jurassicPark = new Park("Jurassic Park", 50000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
            jurassicPark.AddEmployee("Dennis Nedry", 2500, EmployeeRole.IT);
            jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);

            // Act
            string result = jurassicPark.Run(1);

            // Assert
            Assert.That(Regex.Count(result, "--------Increase Salary--------"), Is.EqualTo(1));
            Assert.That(Regex.Count(result, "{ Name: John Hammond, Increase: 200, Salary: 1400200 }"), Is.EqualTo(1));
            Assert.That(Regex.Count(result, "{ Name: Dennis Nedry, Increase: 200, Salary: 2700 }"), Is.EqualTo(0));
        }

        [Test]
        public void ParkSalaryIncreaseWithANegativeScoreTest() {
            // Arrange
            int randomValue = 20;
            _randomService.AddValue(new Tuple<int, int>(0, 100), randomValue);
            Park jurassicPark = new Park("Jurassic Park", 50000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
            jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);

            // Act
            string result = jurassicPark.Run(1);

            // Assert
            Assert.That(Regex.Count(result, "--------Increase Salary--------"), Is.EqualTo(0));
            Assert.That(Regex.Count(result, "{ Name: John Hammond, Increase: 200, Salary: 1400200 }"), Is.EqualTo(0));
        }

        [Test]
        public void ParkSurchargeTest() {
            // Arrange
            int randomValue = 70;
            _randomService.AddValue(new Tuple<int, int>(0, 100), randomValue);
            Park jurassicPark = new Park("Jurassic Park", 50000000, _randomService);

            jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
            jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);

            // Act
            string result = jurassicPark.Run(4);

            // Assert
            Assert.That(Regex.Count(result, "Credit:   2800000"), Is.EqualTo(13));
            Assert.That(Regex.Count(result, "Credit:   2856000"), Is.EqualTo(13));
            Assert.That(Regex.Count(result, "Credit:   2912000"), Is.EqualTo(13));
            Assert.That(Regex.Count(result, "Credit:   2968000"), Is.EqualTo(13));
        }
    }
}