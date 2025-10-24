using naming = LegacyCodeFinalResult._1_Renaming;
using debitCredit = LegacyCodeFinalResult._2_DebitCredit;
using random = LegacyCodeFinalResult._3_Random;
using score = LegacyCodeFinalResult._4_Score;

{
    naming.Park jurassicPark = new naming.Park("Jurassic Park", 50000000);

    jurassicPark.AddEmployee("John Hammond", 1400000, naming.EmployeeRole.Owner);
    jurassicPark.AddEmployee("Henry Wu", 60000, naming.EmployeeRole.Doctor);
    jurassicPark.AddEmployee("Dennis Nedry", 2500, naming.EmployeeRole.IT);
    jurassicPark.AddEmployee("Robert Muldoon", 12500, naming.EmployeeRole.Security);

    jurassicPark.AddEmployee("Donald Gennaro", 20000, naming.EmployeeRole.Insurance);

    jurassicPark.AddEmployee("Alan Grant", 30000, naming.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Elle Sattler", 15000, naming.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Lan Malcolm", 160000, naming.EmployeeRole.Guide);

    jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);
    jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);
    jurassicPark.AddDinosaur("Gallimimus", 120, 250);
    jurassicPark.AddDinosaur("Triceratops", 6, 1000);
    jurassicPark.AddDinosaur("Velociraptor", 4, 2500);
    jurassicPark.AddDinosaur("Dilophosaurus", 8, 1600);
    jurassicPark.AddDinosaur("Parasaurolophus", 24, 500);

    string result = jurassicPark.Run(100);
    Console.WriteLine(result);
} 

{
    debitCredit.Park jurassicPark = new debitCredit.Park("Jurassic Park", 50000000);

    jurassicPark.AddEmployee("John Hammond", 1400000, debitCredit.EmployeeRole.Owner);
    jurassicPark.AddEmployee("Henry Wu", 60000, debitCredit.EmployeeRole.Doctor);
    jurassicPark.AddEmployee("Dennis Nedry", 2500, debitCredit.EmployeeRole.IT);
    jurassicPark.AddEmployee("Robert Muldoon", 12500, debitCredit.EmployeeRole.Security);

    jurassicPark.AddEmployee("Donald Gennaro", 20000, debitCredit.EmployeeRole.Insurance);

    jurassicPark.AddEmployee("Alan Grant", 30000, debitCredit.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Elle Sattler", 15000, debitCredit.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Lan Malcolm", 160000, debitCredit.EmployeeRole.Guide);

    jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);
    jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);
    jurassicPark.AddDinosaur("Gallimimus", 120, 250);
    jurassicPark.AddDinosaur("Triceratops", 6, 1000);
    jurassicPark.AddDinosaur("Velociraptor", 4, 2500);
    jurassicPark.AddDinosaur("Dilophosaurus", 8, 1600);
    jurassicPark.AddDinosaur("Parasaurolophus", 24, 500);

    string result = jurassicPark.Run(100);
    Console.WriteLine(result);
}

{
    random.IRandomService randomService = new random.RandomService();
    random.Park jurassicPark = new random.Park("Jurassic Park", 50000000, randomService);

    jurassicPark.AddEmployee("John Hammond", 1400000, random.EmployeeRole.Owner);
    jurassicPark.AddEmployee("Henry Wu", 60000, random.EmployeeRole.Doctor);
    jurassicPark.AddEmployee("Dennis Nedry", 2500, random.EmployeeRole.IT);
    jurassicPark.AddEmployee("Robert Muldoon", 12500, random.EmployeeRole.Security);

    jurassicPark.AddEmployee("Donald Gennaro", 20000, random.EmployeeRole.Insurance);

    jurassicPark.AddEmployee("Alan Grant", 30000, random.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Elle Sattler", 15000, random.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Lan Malcolm", 160000, random.EmployeeRole.Guide);

    jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);
    jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);
    jurassicPark.AddDinosaur("Gallimimus", 120, 250);
    jurassicPark.AddDinosaur("Triceratops", 6, 1000);
    jurassicPark.AddDinosaur("Velociraptor", 4, 2500);
    jurassicPark.AddDinosaur("Dilophosaurus", 8, 1600);
    jurassicPark.AddDinosaur("Parasaurolophus", 24, 500);

    string result = jurassicPark.Run(100);
    Console.WriteLine(result);
} 

{
    score.IRandomService randomService = new score.RandomService();
    score.Park jurassicPark = new score.Park("Jurassic Park", 50000000, randomService);

    jurassicPark.AddEmployee("John Hammond", 1400000, score.EmployeeRole.Owner);
    jurassicPark.AddEmployee("Henry Wu", 60000, score.EmployeeRole.Doctor);
    jurassicPark.AddEmployee("Dennis Nedry", 2500, score.EmployeeRole.IT);
    jurassicPark.AddEmployee("Robert Muldoon", 12500, score.EmployeeRole.Security);

    jurassicPark.AddEmployee("Donald Gennaro", 20000, score.EmployeeRole.Insurance);

    jurassicPark.AddEmployee("Alan Grant", 30000, score.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Elle Sattler", 15000, score.EmployeeRole.Guide);
    jurassicPark.AddEmployee("Lan Malcolm", 160000, score.EmployeeRole.Guide);

    jurassicPark.AddDinosaur("Tyrannosaurus", 1, 8000);
    jurassicPark.AddDinosaur("Brachiosaurus", 6, 2000);
    jurassicPark.AddDinosaur("Gallimimus", 120, 250);
    jurassicPark.AddDinosaur("Triceratops", 6, 1000);
    jurassicPark.AddDinosaur("Velociraptor", 4, 2500);
    jurassicPark.AddDinosaur("Dilophosaurus", 8, 1600);
    jurassicPark.AddDinosaur("Parasaurolophus", 24, 500);

    string result = jurassicPark.Run(100);
    Console.WriteLine(result);
}

