namespace LegacyCodeFinalResult._3_Random {
    internal class RandomServiceSub : IRandomService {
        private readonly IDictionary<Tuple<int, int>, int> _values;

        public RandomServiceSub() {
            _values = new Dictionary<Tuple<int, int>, int>();
        }

        public int Next(int minValue, int maxValue) {
            int returnValue = minValue;

            if (_values.TryGetValue(new Tuple<int, int>(minValue, maxValue), out int value)) {
                returnValue = value;
            }

            return returnValue;
        }

        public void AddValue(Tuple<int, int> inputs, int value) {
            _values.Add(inputs, value);
        }
    }
}
