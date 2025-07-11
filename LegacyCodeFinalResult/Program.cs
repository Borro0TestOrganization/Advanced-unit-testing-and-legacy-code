using LegacyCodeFinalResult;

Park jurassicPark = new Park("Jurassic Park", 50000000);

jurassicPark.AddEmployee("John Hammond", 1400000, EmployeeRole.Owner);
jurassicPark.AddEmployee("Henry Wu", 60000, EmployeeRole.Doctor);
jurassicPark.AddEmployee("Dennis Nedry", 2500, EmployeeRole.IT);
jurassicPark.AddEmployee("Robert Muldoon", 12500, EmployeeRole.Security);

jurassicPark.AddEmployee("Donald Gennaro", 20000, EmployeeRole.Insurance);

jurassicPark.AddEmployee("Alan Grant", 30000, EmployeeRole.Guide);
jurassicPark.AddEmployee("Elle Sattler", 15000, EmployeeRole.Guide);
jurassicPark.AddEmployee("Lan Malcolm", 160000, EmployeeRole.Guide);

jurassicPark.AddPet("Tyrannosaurus", 1, 8000);
jurassicPark.AddPet("Brachiosaurus", 6, 2000);
jurassicPark.AddPet("Gallimimus", 120, 250);
jurassicPark.AddPet("Triceratops", 6, 1000);
jurassicPark.AddPet("Velociraptor", 4, 2500);
jurassicPark.AddPet("Dilophosaurus", 8, 1600);
jurassicPark.AddPet("Parasaurolophus", 24, 500);

string result = jurassicPark.Run(100);
Console.WriteLine(result);