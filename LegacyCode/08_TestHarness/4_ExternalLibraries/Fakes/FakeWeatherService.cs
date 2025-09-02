namespace LegacyCode._08_TestHarness._4_ExternalLibraries {
    public class FakeWeatherService : IWeatherService {
        private int _currentTemp;
        private string _raining;

        public FakeWeatherService(int currentTemp, string raining) {
            _currentTemp = currentTemp;
            _raining = raining;
        }

        public int GetCurrentTemp() {
            return _currentTemp;
        }

        public string GetExpectedRain() {
            return _raining;
        }

        public bool IsConnected() {
            return true;
        }
    }
}
