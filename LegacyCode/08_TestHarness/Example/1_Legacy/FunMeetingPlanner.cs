using System.IO.Ports;

namespace LegacyCode._08_TestHarness.Example._1_Legacy {
    public class FunMeetingPlanner {
        private WeatherService _weatherService;

        public FunMeetingPlanner(WeatherService weatherService) {
            _weatherService = weatherService;
        }

        public string PlanMeeting() {
            string result = "Let's meet at the office";

            if (_weatherService.GetCurrentTemp() > 20) {
                result = "Let's meet outside!";
            }

            return result;
        }
    }

    public class WeatherService {
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
}
