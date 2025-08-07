namespace LegacyCode._3_MethodWrap.Exercise.Result {
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

        public bool ProcessOrderWithDiscount(Order order) {
            var itemsWithAHighPrice = order.Items.Where(item => item.Price > 50);

            foreach (OrderItem item in itemsWithAHighPrice) {
                item.Price *= 0.8;
            }

            return ProcessOrder(order);
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
