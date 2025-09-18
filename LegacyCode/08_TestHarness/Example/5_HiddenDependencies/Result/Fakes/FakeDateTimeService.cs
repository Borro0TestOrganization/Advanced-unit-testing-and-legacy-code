namespace LegacyCode._08_TestHarness.Example.Result._5_HiddenDependencies {
    public class FakeDateTimeService : IDateTimeService {
        private DateTime _currentDateTime;

        public FakeDateTimeService(DateTime currentDateTime) {
            _currentDateTime = currentDateTime;
        }

        public DateTime GetNow() {
            return _currentDateTime;
        }
    }
}
