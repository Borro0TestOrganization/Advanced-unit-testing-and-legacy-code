using AdvancedUnitTesting.AAAPattern.SlideExample;

namespace AdvancedUnitTesting.AAAPattern.RemoveSharedFixture.Result;


public class AvoidHardCouplingBetweenTests
{
    [Test]
    public void Purchase_succeeds_when_enough_inventory()
    {
        // Arrange
        Store store = CreateStoreWithInventory(Product.Shampoo, 10);
        Customer sut = CreateCustomer();

        // Act
        bool success = sut.Purchase(store, Product.Shampoo, 5);

        // Arrange
        Assert.That(success, Is.True);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(5));
    }
    [Test]
    public void Purchase_fails_when_not_enough_inventory()
    {
        // Arrange
        Store store = CreateStoreWithInventory(Product.Shampoo, 10);
        Customer sut = CreateCustomer();

        // Act
        bool success = sut.Purchase(store, Product.Shampoo, 15);

        // Assert
        Assert.That(success, Is.False);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(10));
    }

    private Store CreateStoreWithInventory(Product product, int quantity)
    {
        Store store = new Store();
        store.AddInventory(product, quantity);
        return store;
    }
    private static Customer CreateCustomer()
    {
        return new Customer();
    }
}
