namespace LegacyCode._4_ClassWrap.Exercise.Result {
    public class ClassWrapResultTests {
        [Test]
        public void OrderProcessorMetricsTest() {
            // Arrange
            Order order = new Order {
                Items = new List<OrderItem>
                {
                    new OrderItem { Price = 100, Quantity = 2 },
                    new OrderItem { Price = 50, Quantity = 1 },
                    new OrderItem { Price = 25, Quantity = 4 }
                }
            };
            OrderProcessor orderProcessor = new OrderProcessor();
            MetricsCollector metricsCollector = new MetricsCollector();
            OrderProcessorMetrics orderProcessorMetrics = new OrderProcessorMetrics(orderProcessor, metricsCollector);

            // Act
            orderProcessorMetrics.ProcessOrder(order);

            // Assert
            Assert.That(order.OrderTotal, Is.EqualTo(350d));
            Assert.That(metricsCollector.GetMetricByKey("order_prosessing"), Is.EqualTo(order));
            Assert.That(metricsCollector.GetMetricByKey("order_prosessed"), Is.EqualTo(order));
        }
    }
}
