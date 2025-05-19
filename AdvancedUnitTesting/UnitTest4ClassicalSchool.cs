using UnitTest4AdvancedCustomerStore;

namespace UnitTest4ClassicalSchool;

public class CustomerTestsWithActualObjects
{
    [Test]
    public void Purchase_succeeds_when_enough_inventory_and_funds()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 5;
        const double unitPrice = 10.00;
        const double totalPrice = unitPrice * quantity;

        var store = new Store();
        store.AddInventory(product, 10);
        var bankAccount = new BankAccount(totalPrice + 5);
        var priceCalculator = new PriceCalculator();
        var sut = new Customer(store, bankAccount, priceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(5));
        Assert.That(store.GetInventory(product), Is.EqualTo(5));
    }

    [Test]
    public void Purchase_fails_when_not_enough_inventory()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 15;

        var store = new Store();
        store.AddInventory(product, 10);
        var bankAccount = new BankAccount(100.0);
        var priceCalculator = new PriceCalculator();
        var sut = new Customer(store, bankAccount, priceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(100.0));
        Assert.That(store.GetInventory(product), Is.EqualTo(10));
    }

    [Test]
    public void Purchase_fails_when_not_enough_funds()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 5;
        const double unitPrice = 10.00;
        const double totalPrice = unitPrice * quantity;

        var store = new Store();
        store.AddInventory(product, 10);
        var bankAccount = new BankAccount(totalPrice - 5);
        var priceCalculator = new PriceCalculator();
        var sut = new Customer(store, bankAccount, priceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(totalPrice - 5));
        Assert.That(store.GetInventory(product), Is.EqualTo(10));
    }
}
