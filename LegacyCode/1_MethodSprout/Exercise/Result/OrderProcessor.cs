using LegacyCode._1_MethodSprout.Exercise.Legacy;
using System.Linq;

namespace LegacyCode._1_MethodSprout.Exercise.Result {
    public class OrderProcessor {
        public bool ProcessOrder(Order order) {
            double totalAmount = 0;
            ProcessDiscount(order.Items);

            foreach (OrderItem item in order.Items) {
                double itemPrice = item.Price * item.Quantity;
                totalAmount += itemPrice;
            }

            order.ProcessedAt = DateTime.Now;
            order.OrderTotal = totalAmount;

            return true;
        }

        public void ProcessDiscount(IList<OrderItem> orderItems) {
            var itemsWithAHighPrice = orderItems.Where(item => item.Price > 50);

            foreach (OrderItem item in itemsWithAHighPrice) {
                item.Price *= 0.8;
            }
        }
    }

    public class Order {
        public IList<OrderItem> Items { get; internal set; }
        public DateTime ProcessedAt { get; internal set; }
        public double OrderTotal { get; internal set; }

        public Order() {
            Items = new List<OrderItem>();
        }
    }

    public class OrderItem {
        public double Price { get; internal set; }
        public int Quantity { get; internal set; }
    }
}
