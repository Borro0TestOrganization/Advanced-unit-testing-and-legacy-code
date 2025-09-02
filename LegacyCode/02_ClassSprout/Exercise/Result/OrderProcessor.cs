namespace LegacyCode._2_ClassSprout.Exercise.Result {
    public class OrderProcessor {
        private readonly IDatabase _database;
        private readonly OrderMetricsCollector _metricsCollector;

        public OrderProcessor(IDatabase database) {
            _database = database;
            _metricsCollector = new OrderMetricsCollector(database);
        }

        public bool ProcessOrder(Order order) {
            DateTime processingTime = DateTime.Now;

            _database.AddOrder(order);
            // ... some order processing logic ...

            _metricsCollector.CollectMetricsForOrder(order, processingTime);

            return true;
        }
    }

    public class OrderMetricsCollector {
        private readonly IDatabase _database;

        public OrderMetricsCollector(IDatabase database) {
            _database = database;
        }

        public void CollectMetricsForOrder(Order order, DateTime processingTime) {
            var todayOrders = _database.GetOrdersCreatedToday();
            var totalRevenue = todayOrders.Sum(o => o.TotalAmount());
            // ... other metrics related data ...

            _database.SaveMetric("process_time", processingTime);
            _database.SaveMetric("daily_orders", todayOrders.Count);
            _database.SaveMetric("daily_revenue", totalRevenue);
            _database.SaveMetric("order_item_count", order.Items.Sum(item => item.Quantity));
            // ... other metrics storage processes ...
        }
    }

    public interface IDatabase {
        IList<Order> GetOrdersCreatedToday();
        void AddOrder(Order order);
        void SaveMetric(string key, object value);
        object GetMetricByKey(string key);
    }

    public class Database : IDatabase {
        IList<Order> _orders;
        IDictionary<string, object> _metrics;

        public Database() {
            _orders = new List<Order>();
            _metrics = new Dictionary<string, object>();
        }

        public void AddOrder(Order order) {
            _orders.Add(order);
        }

        public IList<Order> GetOrdersCreatedToday() {
            return _orders;
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
