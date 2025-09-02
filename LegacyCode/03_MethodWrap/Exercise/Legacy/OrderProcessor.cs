namespace LegacyCode._3_MethodWrap.Exercise.Legacy {
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
