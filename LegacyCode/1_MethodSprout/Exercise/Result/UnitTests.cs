namespace LegacyCode._1_MethodSprout.Exercise.Result {
    public class MethodSproutResultTests {
        [Test]
        public void ProcessOrderWithDiscount() {
            // Arrange
            Order order = new Order {
                Items = new List<OrderItem>
                {
                    new OrderItem { Price = 100, Quantity = 2 },
                    new OrderItem { Price = 50, Quantity = 1 },
                    new OrderItem { Price = 25, Quantity = 4 }
                }
            };
            OrderProcessor processor = new OrderProcessor();

            // Act
            processor.ProcessOrder(order);

            // Assert
            Assert.That(order.OrderTotal, Is.EqualTo(310));
        }

        [Test]
        public void ProcessOrderItemsWithDiscount() {
            // Arrange
            IList<OrderItem> items = new List<OrderItem>
            {
                new OrderItem { Price = 100, Quantity = 2 },
                new OrderItem { Price = 50, Quantity = 1 },
                new OrderItem { Price = 25, Quantity = 4 }
            };
            OrderProcessor processor = new OrderProcessor();

            // Act
            processor.ProcessDiscount(items);

            // Assert
            Assert.That(items[0].Price, Is.EqualTo(80));
            Assert.That(items[1].Price, Is.EqualTo(50));
            Assert.That(items[2].Price, Is.EqualTo(25));
        }
    }
}
