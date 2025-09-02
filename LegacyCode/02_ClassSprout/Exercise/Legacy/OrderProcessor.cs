namespace LegacyCode._2_ClassSprout.Exercise.Legacy {
    public class OrderProcessor {
        private readonly IDatabase _database;

        public OrderProcessor(IDatabase database) {
            _database = database;
        }

        public bool ProcessOrder(Order order) {
            DateTime processingTime = DateTime.Now;

            _database.AddOrder(order);
            // ... some order processing logic ...

            var todayOrders = _database.GetOrdersCreatedToday();
            var totalRevenue = todayOrders.Sum(o => o.TotalAmount());
            // ... other metrics related data ...

            _database.SaveMetric("process_time", processingTime);
            _database.SaveMetric("daily_orders", todayOrders.Count);
            _database.SaveMetric("daily_revenue", totalRevenue);
            // ... other metrics storage processes ...

            return true;
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
