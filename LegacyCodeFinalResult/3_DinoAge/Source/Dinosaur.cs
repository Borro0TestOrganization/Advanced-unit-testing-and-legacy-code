namespace LegacyCodeFinalResult._3_DinoAge.Source {
    internal class Dinosaur {
        public string Type { get; private set; }
        public int Age { get; private set; }
        public decimal Costs { get; private set; }

        public Dinosaur(string type, int age, decimal costs) {
            Type = type;
            Age = age;
            Costs = costs;
        }

        public void BecomeOlder() {
            Age++;
        }
    }
}
