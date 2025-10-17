using NUnit.Framework.Interfaces;

namespace LegacyCode._10_ClassIsTooHuge.Exercise.Result {
    public class OrderProcessor {
        private IOrderRepository _orderRepository;
        private IProductRepository _productRepository;
        private ICustomerRepository _customerRepository;

        public OrderProcessor(
            IOrderRepository orderRepository, 
            IProductRepository productRepository, 
            ICustomerRepository customerRepository) {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public bool CreateOrder(int customerId, int orderId) {
            bool result = false;

            if (_customerRepository.GetCustomer(customerId) != null) {
                Order newOrder = new Order(orderId, customerId, new List<OrderItem>());
                result = _orderRepository.SaveOrder(newOrder);
            }

            return result;
        }

        public bool AddItemToOrder(int orderId, int itemId, int quantity) {
            bool result = false;
            Order order = _orderRepository.GetOrder(orderId);
            Product product = _productRepository.GetProduct(itemId);

            if (order != null && product != null && product.Stock >= quantity) {
                order.Items.Add(new OrderItem(itemId, quantity));
                _productRepository.ReduceStock(itemId, quantity);
                _orderRepository.UpdateOrderItems(orderId, order.Items);
                result = true;
            }

            return result;
        }

        public bool RemoveItemFromOrder(int orderId, int itemId) {
            bool result = false;
            Order order = _orderRepository.GetOrder(orderId);

            if (order != null) {
                OrderItem itemToRemove = order.Items.FirstOrDefault(item => item.ItemId == itemId);

                if (itemToRemove != null) {
                    _productRepository.IncreaseStock(itemId, itemToRemove.Quantity);
                    order.Items.Remove(itemToRemove);
                    _orderRepository.UpdateOrderItems(orderId, order.Items);
                    result = true;
                }
            }

            return result;
        }

        public bool UpdateOrderStatus(int orderId, string newStatus) {
            bool result = _orderRepository.UpdateStatus(orderId, newStatus);
            return result;
        }

        public bool ProcessPayment(int orderId, decimal amount) {
            bool result = false;
            Order order = _orderRepository.GetOrder(orderId);

            if (order != null && order.Status == "New") {
                bool paymentSuccess = amount > 0;

                if (paymentSuccess) {
                    result = _orderRepository.UpdateStatus(orderId, "Paid");
                }
            }

            return result;
        }

        public bool ShipOrder(int orderId, string trackingNumber) {
            bool result = false;
            Order order = _orderRepository.GetOrder(orderId);

            if (order != null && order.Status == "Paid") {
                result = _orderRepository.UpdateShippingInfo(orderId, "Shipped", trackingNumber);
            }

            return result;
        }

        public bool CancelOrder(int orderId) {
            bool result = false;
            Order order = _orderRepository.GetOrder(orderId);

            if (order != null && order.Status != "Shipped") {
                foreach (OrderItem item in order.Items) {
                    _productRepository.IncreaseStock(item.ItemId, item.Quantity);
                }

                result = _orderRepository.DeleteOrder(orderId);
            }

            return result;
        }

        public Order GetOrderDetails(int orderId) {
            Order order = _orderRepository.GetOrder(orderId);
            return order;
        }

        public bool AddCustomer(int customerId, string name, string address) {
            Customer newCustomer = new Customer(customerId, name, address);
            bool result = _customerRepository.SaveCustomer(newCustomer);
            return result;
        }

        public Customer GetCustomerInfo(int customerId) {
            Customer customer = _customerRepository.GetCustomer(customerId);
            return customer;
        }

        public bool UpdateCustomerInfo(int customerId, string newName = null, string newAddress = null) {
            bool result = _customerRepository.UpdateCustomer(customerId, newName, newAddress);
            return result;
        }

        public bool AddProduct(int productId, string name, decimal price, int stock) {
            Product newProduct = new Product(productId, name, price, stock);
            bool result = _productRepository.SaveProduct(newProduct);
            return result;
        }

        public Product GetProductInfo(int productId) {
            Product product = _productRepository.GetProduct(productId);
            return product;
        }
    }

    public interface IOrderRepository {
        bool SaveOrder(Order order);
        Order GetOrder(int orderId);
        bool UpdateStatus(int orderId, string newStatus);
        bool UpdateShippingInfo(int orderId, string status, string trackingNumber);
        bool UpdateOrderItems(int orderId, List<OrderItem> items);
        bool DeleteOrder(int orderId);
    }

    public interface IProductRepository {
        Product GetProduct(int productId);
        bool SaveProduct(Product product);
        bool ReduceStock(int productId, int quantity);
        bool IncreaseStock(int productId, int quantity);
    }

    public interface ICustomerRepository {
        Customer GetCustomer(int customerId);
        bool SaveCustomer(Customer customer);
        bool UpdateCustomer(int customerId, string newName, string newAddress);
    }

    public class OrderRepository : IOrderRepository {
        private Dictionary<int, Order> _orders;

        public OrderRepository() { 
            _orders = new Dictionary<int, Order>();
        }

        public bool UpdateOrderItems(int orderId, List<OrderItem> items) {
            bool result = false;

            if (_orders.ContainsKey(orderId)) {
                _orders[orderId].Items = items;
                result = true;
            }

            return result;
        }

        public bool DeleteOrder(int orderId) {
            return _orders.Remove(orderId);
        }

        public bool SaveOrder(Order order) {
            return _orders.TryAdd(order.OrderId, order);
        }

        public Order GetOrder(int orderId) {
            return _orders[orderId];
        }

        public bool UpdateStatus(int orderId, string newStatus) {
            bool result = false;

            if (_orders.ContainsKey(orderId)) {
                _orders[orderId].Status = newStatus;
                result = true;
            }

            return result;
        }

        public bool UpdateShippingInfo(int orderId, string status, string trackingNumber) {
            bool result = false;

            if (_orders.ContainsKey(orderId)) {
                _orders[orderId].Status = status;
                _orders[orderId].TrackingNumber = trackingNumber;
                result = true;
            }

            return result;
        }
    }

    public class ProductRepository : IProductRepository {
        private Dictionary<int, Product> _products;

        public ProductRepository() {
            _products = new Dictionary<int, Product>();
        }

        public bool SaveProduct(Product product) {
            bool result;

            if (!_products.ContainsKey(product.ProductId)) {
                _products[product.ProductId] = product;
                result = true;
            } else {
                result = false;
            }

            return result;
        }

        public bool IncreaseStock(int productId, int quantity) {
            bool result = false;

            if (_products.ContainsKey(productId)) {
                _products[productId].Stock += quantity;
                result = true;
            }

            return result;
        }

        public Product GetProduct(int productId) {
            return _products[productId];
        }

        public bool ReduceStock(int productId, int quantity) {
            bool result = false;

            if (_products.ContainsKey(productId)) {
                _products[productId].Stock = quantity;
                result = true;
            }

            return result;
        }
    }

    public class CustomerRepository : ICustomerRepository {
        private Dictionary<int, Customer> _customers;

        public CustomerRepository() {
            _customers = new Dictionary<int, Customer>();
        }

        public Customer GetCustomer(int customerId) {
            return _customers[customerId];
        }

        public bool SaveCustomer(Customer customer) {
            return _customers.TryAdd(customer.CustomerId, customer);
        }

        public bool UpdateCustomer(int customerId, string newName, string newAddress) {
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
    }

    public class Order {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public string Status { get; set; }
        public string TrackingNumber { get; set; }

        public Order(int orderId, int customerId, List<OrderItem> items) {
            OrderId = orderId;
            CustomerId = customerId;
            Items = items;
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
