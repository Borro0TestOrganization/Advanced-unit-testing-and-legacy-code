namespace LegacyCode._08_TestHarness._4_ExternalLibraries {
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
