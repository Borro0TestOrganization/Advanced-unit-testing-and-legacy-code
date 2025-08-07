namespace LegacyCode._3_MethodWrap.Exercise.Result {
    public class MethodSproutResultTests {
        [Test]
        public void TestProcessOrderWithDiscount() {
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
            processor.ProcessOrderWithDiscount(order);

            // Assert
            Assert.That(order.OrderTotal, Is.EqualTo(310));
        }

        [Test]
        public void TestProcessOrder() {
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
            Assert.That(order.OrderTotal, Is.EqualTo(350));
        }
    }
}
