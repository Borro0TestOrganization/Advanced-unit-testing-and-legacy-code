namespace LegacyCode._5_Dependencies.Exercise.Legacy {
    public class OrderProcessor {
        private FileLogger _logger = new FileLogger("log.txt");

        public bool ProcessOrder(Order order) {
            _logger.Log(LogLevel.Info, "Start order processing...");
            double totalAmount = 0;

            foreach (OrderItem item in order.Items) {
                double itemPrice = item.Price * item.Quantity;
                totalAmount += itemPrice;

                _logger.Log(LogLevel.Trace, $"Add item price: {item.Price}");
                _logger.Log(LogLevel.Trace, $"Order amout is at: {totalAmount}");
            }

            order.ProcessedAt = DateTime.Now;
            order.OrderTotal = totalAmount;

            _logger.Log(LogLevel.Info, "Finished order processing");
            return true;
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
