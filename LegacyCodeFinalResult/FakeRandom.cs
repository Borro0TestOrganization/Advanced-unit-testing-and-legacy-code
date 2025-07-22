namespace LegacyCodeFinalResult
{
    public class FakeRandom(double rangeSelector) : IRandom
    {
        public int Next(int minValue, int maxValue)
        {
            int range = maxValue - minValue;
            return (int)(range * RangeSelector) + minValue;
        }

        public double RangeSelector { get; set; } = rangeSelector;
    }
}
