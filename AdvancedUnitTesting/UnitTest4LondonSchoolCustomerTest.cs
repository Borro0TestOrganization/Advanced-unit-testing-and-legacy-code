using NSubstitute;
using UnitTest4AdvancedCustomerStore;

namespace UnitTest4LondonSchoolTest;

[TestFixture]
public class CustomerTests
{
    private Product _testProduct = Product.Shampoo;
    private int _testQuantity = 2;
    private double _calculatedPrice = 18.00; // Example: 9.00 * 2 for Premium Shampoo

    [Test]
    public void Purchase_WhenStoreHasEnoughInventoryAndBankAccountHasEnoughFunds_ShouldSucceed()
    {
        // Arrange
        var store = Substitute.For<IStore>();
        var bankAccount = Substitute.For<IBankAccount>();
        var priceCalculator = Substitute.For<IPriceCalculator>();

        // Configure substitutes
        store.HasEnoughInventory(_testProduct, _testQuantity).Returns(true);
        priceCalculator.CalculatePrice(_testProduct, MembershipType.Premium).Returns(_calculatedPrice / _testQuantity); // Unit price

        var customer = new Customer(store, bankAccount, priceCalculator, MembershipType.Premium);

        // Act
        var result = customer.Purchase(_testProduct, _testQuantity);

        // Assert
        Assert.That(result, Is.True);
        store.Received(1).HasEnoughInventory(_testProduct, _testQuantity);
        priceCalculator.Received(1).CalculatePrice(_testProduct, MembershipType.Premium);
        bankAccount.Received(1).Withdraw(_calculatedPrice);
        store.Received(1).RemoveInventory(_testProduct, _testQuantity);
    }

    [Test]
    public void Purchase_WhenStoreDoesNotHaveEnoughInventory_ShouldReturnFalseAndNotProceedWithTransaction()
    {
        // Arrange
        var store = Substitute.For<IStore>();
        var bankAccount = Substitute.For<IBankAccount>();
        var priceCalculator = Substitute.For<IPriceCalculator>();

        // Configure substitutes
        store.HasEnoughInventory(_testProduct, _testQuantity).Returns(false);

        var customer = new Customer(store, bankAccount, priceCalculator, MembershipType.Premium);

        // Act
        var result = customer.Purchase(_testProduct, _testQuantity);

        // Assert
        Assert.That(result, Is.False);
        store.Received(1).HasEnoughInventory(_testProduct, _testQuantity);
        priceCalculator.DidNotReceive().CalculatePrice(Arg.Any<Product>(), Arg.Any<MembershipType>());
        bankAccount.DidNotReceive().Withdraw(Arg.Any<double>());
        store.DidNotReceive().RemoveInventory(Arg.Any<Product>(), Arg.Any<int>());
    }

    [Test]
    public void Purchase_WhenBankAccountHasInsufficientFunds_ShouldReturnFalseAndNotRemoveInventory()
    {
        // Arrange
        var store = Substitute.For<IStore>();
        var bankAccount = Substitute.For<IBankAccount>();
        var priceCalculator = Substitute.For<IPriceCalculator>();

        // Configure substitutes
        store.HasEnoughInventory(_testProduct, _testQuantity).Returns(true);
        priceCalculator.CalculatePrice(_testProduct, MembershipType.Premium).Returns(_calculatedPrice / _testQuantity); // Unit price
        bankAccount.When(ba => ba.Withdraw(Arg.Any<double>())).Throw(new InvalidOperationException("Insufficient funds"));

        var customer = new Customer(store, bankAccount, priceCalculator, MembershipType.Premium);

        // Act
        var result = customer.Purchase(_testProduct, _testQuantity);

        // Assert
        Assert.That(result, Is.False);
        store.Received(1).HasEnoughInventory(_testProduct, _testQuantity);
        priceCalculator.Received(1).CalculatePrice(_testProduct, MembershipType.Premium);
        bankAccount.Received(1).Withdraw(_calculatedPrice);
        store.DidNotReceive().RemoveInventory(Arg.Any<Product>(), Arg.Any<int>()); // Inventory should NOT be removed
    }

    [TestCase(MembershipType.Regular, Product.Shampoo, 10.00, 3, 30.00)]
    [TestCase(MembershipType.Premium, Product.Book, 18.00, 1, 18.00)]
    public void Purchase_ShouldCalculatePriceCorrectlyBasedOnMembershipAndProduct(MembershipType membershipType, Product product, double unitPrice, int quantity, double expectedTotalPrice)
    {
        // Arrange
        var store = Substitute.For<IStore>();
        var bankAccount = Substitute.For<IBankAccount>();
        var priceCalculator = Substitute.For<IPriceCalculator>();

        // Configure substitutes
        store.HasEnoughInventory(product, quantity).Returns(true);
        priceCalculator.CalculatePrice(product, membershipType).Returns(unitPrice);

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.True);
        priceCalculator.Received(1).CalculatePrice(product, membershipType);
        bankAccount.Received(1).Withdraw(expectedTotalPrice);
        store.Received(1).RemoveInventory(product, quantity);
    }
}
