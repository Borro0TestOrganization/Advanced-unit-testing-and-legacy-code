namespace LegacyCodeFinalResult._3_DinoAge {
    internal class Dinosaur {
        public int Age { get; private set; }

        public Dinosaur(int age) {
            Age = age;
        }

        public void BecomeOlder() {
            Age++;
        }
    }
}
