namespace LegacyCode._5_Dependencies.Exercise.Result {
    public class OrderProcessor {
        private ILogger _logger;

        public OrderProcessor(ILogger logger) {
            _logger = logger;
        }

        public bool ProcessOrder(Order order) {
            _logger.LogInfo("Start order processing...");
            double totalAmount = 0;

            foreach (OrderItem item in order.Items) {
                double itemPrice = item.Price * item.Quantity;
                totalAmount += itemPrice;

                _logger.LogTrace($"Add item price: {item.Price}");
                _logger.LogTrace($"Order amout is at: {totalAmount}");
            }

            order.ProcessedAt = DateTime.Now;
            order.OrderTotal = totalAmount;

            _logger.LogInfo("Finished order processing");
            return true;
        }
    }

    public interface ILogger {
        void LogTrace(string traceMessage);
        void LogDebug(string debugMessage);
        void LogInfo(string infoMessage);
    }

    public class FileLoggerAdapter : ILogger {
        private FileLogger _logger = new FileLogger("log.txt");

        public void LogTrace(string traceMessage) {
            _logger.Log(LogLevel.Trace, traceMessage);
        }

        public void LogDebug(string debugMessage) {
            _logger.Log(LogLevel.Debug, debugMessage);
        }

        public void LogInfo(string infoMessage) {
            _logger.Log(LogLevel.Info, infoMessage);
        }
    }

    public class FileLogger {
        private string _logFile;

        public FileLogger(string logFile) {
            _logFile = logFile;
        }

        public void Log(LogLevel logLevel, string message) {
            using (StreamWriter outputFile = new StreamWriter(_logFile)) {
                outputFile.WriteLine(message);
            }
        }
    }

    public enum LogLevel { Trace, Debug, Info };

    public class Order {
        public IList<OrderItem> Items { get; internal set; }
        public DateTime ProcessedAt { get; internal set; }
        public double OrderTotal { get; internal set; }
    }

    public class OrderItem {
        public double Price { get; internal set; }
        public int Quantity { get; internal set; }
    }
}
