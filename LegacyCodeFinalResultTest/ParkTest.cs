using LegacyCodeFinalResult;

namespace LegacyCodeFinalResultTest
{
    public class ParkTest
    {
        [Test]
        public void JurassicParkTest()
        {
            // Arrange
            Park jurassicPark = new Park("Jurassic Park", 50000000);

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

            Console.WriteLine(result);

            // Assert
            Assert.IsNotEmpty(result);
        }

        [Test]
        public void SiteBTest()
        {
            // Arrange
            Park jurassicPark = new Park("Site B", 650000000);

            jurassicPark.AddEmployee("John Hammond", 450000, EmployeeRole.Owner);
            jurassicPark.AddEmployee("Dinosaurer Ludlow", 250000, EmployeeRole.Owner);

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
    }
}
