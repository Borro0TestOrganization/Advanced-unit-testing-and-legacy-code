namespace LegacyCodeFinalResult
{
    public class RealRandom : IRandom
    {
        public int Next(int minValue, int maxValue)
        {
            return new Random().Next(minValue, maxValue);
        }
    }
}
