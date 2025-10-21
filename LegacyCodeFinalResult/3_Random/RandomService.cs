namespace LegacyCodeFinalResult._3_Random {
    internal class RandomService : IRandomService {
        private Random _random;

        public RandomService() { 
            _random = new Random();
        }

        public int Next(int minValue, int maxValue) {
            return _random.Next(minValue, maxValue);
        }
    }
}
