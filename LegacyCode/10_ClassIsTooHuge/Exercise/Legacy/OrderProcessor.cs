namespace LegacyCode._10_ClassIsTooHuge.Exercise.Legacy {
    public class OrderProcessor {
        private Dictionary<int, Order> _orders;
        private Dictionary<int, Product> _products;
        private Dictionary<int, Customer> _customers;

        public OrderProcessor() {
            _orders = new Dictionary<int, Order>();
            _products = new Dictionary<int, Product>();
            _customers = new Dictionary<int, Customer>();
        }

        public bool CreateOrder(int customerId, int orderId) {
            bool result;

            if (!_orders.ContainsKey(orderId)) {
                _orders[orderId] = new Order(orderId, customerId);
                result = true;
            } else {
                result = false;
            }

            return result;
        }

        public bool AddItemToOrder(int orderId, int itemId, int quantity) {
            bool result = false;

            if (_orders.ContainsKey(orderId) && _products.ContainsKey(itemId)) {
                if (_products[itemId].Stock >= quantity) {
                    _orders[orderId].Items.Add(new OrderItem(itemId, quantity));
                    _products[itemId].Stock -= quantity;
                    result = true;
                }
            }

            return result;
        }

        public bool RemoveItemFromOrder(int orderId, int itemId) {
            bool result = false;

            if (_orders.ContainsKey(orderId)) {
                Order order = _orders[orderId];
                OrderItem itemToRemove = order.Items.FirstOrDefault(item => item.ItemId == itemId);

                if (itemToRemove != null) {
                    _products[itemId].Stock += itemToRemove.Quantity;
                    order.Items.Remove(itemToRemove);
                    result = true;
                }
            }

            return result;
        }

        public bool UpdateOrderStatus(int orderId, string newStatus) {
            bool result = false;

            if (_orders.ContainsKey(orderId)) {
                _orders[orderId].Status = newStatus;
                result = true;
            }

            return result;
        }

        public bool ProcessPayment(int orderId, decimal amount) {
            bool result = false;

            if (_orders.ContainsKey(orderId) && _orders[orderId].Status == "New") {
                _orders[orderId].Status = "Paid";
                result = true;
            }

            return result;
        }

        public bool ShipOrder(int orderId, string trackingNumber) {
            bool result = false;

            if (_orders.ContainsKey(orderId) && _orders[orderId].Status == "Paid") {
                _orders[orderId].Status = "Shipped";
                _orders[orderId].TrackingNumber = trackingNumber;
                result = true;
            }

            return result;
        }

        public bool CancelOrder(int orderId) {
            bool result = false;

            if (_orders.ContainsKey(orderId) && _orders[orderId].Status != "Shipped") {
                foreach (OrderItem item in _orders[orderId].Items) {
                    _products[item.ItemId].Stock += item.Quantity;
                }

                _orders.Remove(orderId);
                result = true;
            }

            return result;
        }

        public Order GetOrderDetails(int orderId) {
            Order order;

            _orders.TryGetValue(orderId, out order);

            return order;
        }

        public bool AddCustomer(int customerId, string name, string address) {
            bool result;

            if (!_customers.ContainsKey(customerId)) {
                _customers[customerId] = new Customer(customerId, name, address);
                result = true;
            } else {
                result = false;
            }

            return result;
        }

        public Customer GetCustomerInfo(int customerId) {
            Customer customer;

            _customers.TryGetValue(customerId, out customer);

            return customer;
        }

        public bool UpdateCustomerInfo(int customerId, string newName = null, string newAddress = null) {
            bool result = false;

            if (_customers.ContainsKey(customerId)) {
                if (newName != null) {
                    _customers[customerId].Name = newName;
                }

                if (newAddress != null) {
                    _customers[customerId].Address = newAddress;
                }

                result = true;
            }

            return result;
        }

        public bool AddProduct(int productId, string name, decimal price, int stock) {
            bool result;

            if (!_products.ContainsKey(productId)) {
                _products[productId] = new Product(productId, name, price, stock);
                result = true;
            } else {
                result = false;
            }

            return result;
        }

        public Product GetProductInfo(int productId) {
            Product product;

            _products.TryGetValue(productId, out product);

            return product;
        }
    }

    public class Order {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }

        public Order(int orderId, int customerId) {
            OrderId = orderId;
            CustomerId = customerId;
            Items = new List<OrderItem>();
            Status = "New";
        }
    }

    public class OrderItem {
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int itemId, int quantity) {
            ItemId = itemId;
            Quantity = quantity;
        }
    }

    public class Customer {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public Customer(int customerId, string name, string address) {
            CustomerId = customerId;
            Name = name;
            Address = address;
        }
    }

    public class Product {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product(int productId, string name, decimal price, int stock) {
            ProductId = productId;
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
