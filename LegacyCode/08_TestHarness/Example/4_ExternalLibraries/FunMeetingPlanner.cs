using System.IO.Ports;

namespace LegacyCode._08_TestHarness.Example._4_ExternalLibraries {
    public class FunMeetingPlanner {
        private IWeatherService _weatherService;
        private IDateTimeService _dateTimeService;

        public FunMeetingPlanner(IWeatherService weatherService, IDateTimeService dateTimeService) {
            _weatherService = weatherService;
            _dateTimeService = dateTimeService;
        }

        public string PlanMeeting() {
            string result = "Let's meet at the office";

            if (_dateTimeService.GetNow().DayOfWeek == DayOfWeek.Sunday) {
                result = "Sunday's we don't meet!";
            } else if (_weatherService.GetCurrentTemp() > 20) {
                result = "Let's meet outside!";
            }

            return result;
        }
    }

    public interface IWeatherService {
        int GetCurrentTemp();
        string GetExpectedRain();
        bool IsConnected();
    }

    public class WeatherService : IWeatherService {
        private SerialPort _serialPort;

        public WeatherService(string portName, int baudRate) {
            _serialPort = new SerialPort(portName);
            _serialPort.BaudRate = baudRate;
        }

        public int GetCurrentTemp() {
            return int.Parse(RunCommand("T"));
        }

        public string GetExpectedRain() {
            return RunCommand("R");
        }

        public bool IsConnected() {
            return _serialPort.IsOpen;
        }

        private string RunCommand(string command) {
            string result = string.Empty;
            Connect();

            if (IsConnected()) {
                _serialPort.WriteLine(command);
                result = _serialPort.ReadLine();
                Disconnect();
            }

            return result;
        }

        private void Connect() {
            _serialPort.Open();
            Thread.Sleep(2000); // Slow, bad for tests
        }

        private void Disconnect() {
            _serialPort.Close();
        }
    }

    public interface IDateTimeService {
        public DateTime GetNow();
    }

    public class DateTimeService : IDateTimeService {
        public DateTime GetNow() {
            return DateTime.Now;
        }
    }
}
