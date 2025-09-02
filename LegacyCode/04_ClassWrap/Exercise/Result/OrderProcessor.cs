namespace LegacyCode._4_ClassWrap.Exercise.Result {
    public class OrderProcessor {
        public bool ProcessOrder(Order order) {
            double totalAmount = 0;

            foreach (OrderItem item in order.Items) {
                double itemPrice = item.Price * item.Quantity;
                totalAmount += itemPrice;
            }

            order.ProcessedAt = DateTime.Now;
            order.OrderTotal = totalAmount;

            return true;
        }
    }

    public class OrderProcessorMetrics {
        private OrderProcessor _orderProcessor;
        private MetricsCollector _metricsCollector;

        public OrderProcessorMetrics(OrderProcessor orderProcessor, MetricsCollector metricsCollector) {
            _orderProcessor = orderProcessor;
            _metricsCollector = metricsCollector;
        }

        public bool ProcessOrder(Order order) {
            _metricsCollector.SaveMetric("order_prosessing", order);

            bool processOrderResult = _orderProcessor.ProcessOrder(order);

            _metricsCollector.SaveMetric("order_prosessed", order);

            return processOrderResult;
        }
    }

    public class MetricsCollector {
        IDictionary<string, object> _metrics;

        public MetricsCollector() {
            _metrics = new Dictionary<string, object>();
        }

        public void SaveMetric(string key, object value) {
            _metrics.Add(key, value);
        }

        public object GetMetricByKey(string key) {
            return _metrics[key];
        }
    }

    public class Order {
        public IList<OrderItem> Items { get; internal set; }
        public DateTime ProcessedAt { get; internal set; }
        public double OrderTotal { get; internal set; }

        public double TotalAmount() {
            double totalAmount = 0;

            foreach (OrderItem orderItem in Items) {
                totalAmount += orderItem.Price * orderItem.Quantity;
            }

            return totalAmount;
        }
    }

    public class OrderItem {
        public double Price { get; internal set; }
        public int Quantity { get; internal set; }
    }
}
