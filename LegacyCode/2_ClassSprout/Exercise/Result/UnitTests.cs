namespace LegacyCode._2_ClassSprout.Exercise.Result {
    public class ClassSproutResultTests {
        [Test]
        public void OrderMetricsCollectorWithEmptyDatabaseTest() {
            // Arrange
            IDatabase database = new Database();
            DateTime processingTime = DateTime.Parse("2025-7-11");
            Order order = new Order {
                Items = new List<OrderItem>
                {
                    new OrderItem { Price = 100, Quantity = 2 },
                    new OrderItem { Price = 50, Quantity = 1 },
                    new OrderItem { Price = 25, Quantity = 4 }
                }
            };

            OrderMetricsCollector orderMetricsCollector = new OrderMetricsCollector(database);

            // Act
            orderMetricsCollector.CollectMetricsForOrder(order, processingTime);

            // Assert
            Assert.That(database.GetMetricByKey("process_time"), Is.EqualTo(processingTime));
            Assert.That(database.GetMetricByKey("daily_orders"), Is.EqualTo(0));
            Assert.That(database.GetMetricByKey("daily_revenue"), Is.EqualTo(0));
            Assert.That(database.GetMetricByKey("order_item_count"), Is.EqualTo(7));
        }

        [Test]
        public void OrderMetricsCollectorWithAnOrderInsideTheDatabaseTest() {
            // Arrange
            IDatabase database = new Database();
            DateTime processingTime = DateTime.Parse("2025-7-11");
            Order order = new Order {
                Items = new List<OrderItem>
                {
                    new OrderItem { Price = 100, Quantity = 2 },
                    new OrderItem { Price = 50, Quantity = 1 },
                    new OrderItem { Price = 25, Quantity = 4 }
                }
            };
            database.AddOrder(order);

            OrderMetricsCollector orderMetricsCollector = new OrderMetricsCollector(database);

            // Act
            orderMetricsCollector.CollectMetricsForOrder(order, processingTime);

            // Assert
            Assert.That(database.GetMetricByKey("process_time"), Is.EqualTo(processingTime));
            Assert.That(database.GetMetricByKey("daily_orders"), Is.EqualTo(1));
            Assert.That(database.GetMetricByKey("daily_revenue"), Is.EqualTo(350));
            Assert.That(database.GetMetricByKey("order_item_count"), Is.EqualTo(7));
        }
    }
}
