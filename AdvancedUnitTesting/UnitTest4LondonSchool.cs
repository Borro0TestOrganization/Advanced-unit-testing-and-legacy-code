using UnitTest4AdvancedCustomerStore;
using NSubstitute;

namespace UnitTest4ResultLondonSchool;

public class CustomerTests
{
    [Test]
    public void Purchase_succeeds_when_enough_inventory_and_funds()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 5;
        const double unitPrice = 10.00;
        const double totalPrice = unitPrice * quantity;

        var mockStore = Substitute.For<Store>(null);
        mockStore.HasEnoughInventory(product, quantity).Returns(true);
        mockStore.GetInventory(product).Returns(10 - quantity); // Simulate inventory after removal

        var mockBankAccount = Substitute.For<BankAccount>(0.0);
        mockBankAccount.CurrentBalance.Returns(totalPrice + 5);

        var mockPriceCalculator = Substitute.For<PriceCalculator>();
        mockPriceCalculator.CalculatePrice(product, MembershipType.Regular).Returns(unitPrice);

        var sut = new Customer(mockStore, mockBankAccount, mockPriceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.True);
        mockBankAccount.Received(1).Withdraw(totalPrice);
        mockStore.Received(1).RemoveInventory(product, quantity);
        mockStore.Received(1).GetInventory(product);
    }

    [Test]
    public void Purchase_fails_when_not_enough_inventory()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 15;

        var mockStore = Substitute.For<Store>(null);
        mockStore.HasEnoughInventory(product, quantity).Returns(false);
        mockStore.GetInventory(product).Returns(10); // Initial inventory

        var mockBankAccount = Substitute.For<BankAccount>(0.0);
        mockBankAccount.CurrentBalance.Returns(100.0);

        var mockPriceCalculator = Substitute.For<PriceCalculator>();
        mockPriceCalculator.CalculatePrice(product, MembershipType.Regular).Returns(10.00);

        var sut = new Customer(mockStore, mockBankAccount, mockPriceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.False);
        mockBankAccount.DidNotReceive().Withdraw(Arg.Any<double>());
        mockStore.DidNotReceive().RemoveInventory(product, quantity);
        mockStore.Received(1).GetInventory(product);
    }

    [Test]
    public void Purchase_fails_when_not_enough_funds()
    {
        // Arrange
        const Product product = Product.Shampoo;
        const int quantity = 5;
        const double unitPrice = 10.00;
        const double totalPrice = unitPrice * quantity;

        var mockStore = Substitute.For<Store>(null);
        mockStore.HasEnoughInventory(product, quantity).Returns(true);
        mockStore.GetInventory(product).Returns(10); // Initial inventory

        var mockBankAccount = Substitute.For<BankAccount>(0.0);
        mockBankAccount.CurrentBalance.Returns(totalPrice - 5);
        mockBankAccount.When(b => b.Withdraw(totalPrice)).Do(x => throw new InvalidOperationException("Insufficient funds"));

        var mockPriceCalculator = Substitute.For<PriceCalculator>();
        mockPriceCalculator.CalculatePrice(product, MembershipType.Regular).Returns(unitPrice);

        var sut = new Customer(mockStore, mockBankAccount, mockPriceCalculator, MembershipType.Regular);

        // Act
        bool success = sut.Purchase(product, quantity);

        // Assert
        Assert.That(success, Is.False);
        mockBankAccount.Received(1).Withdraw(totalPrice);
        mockStore.DidNotReceive().RemoveInventory(product, quantity);
        mockStore.Received(1).GetInventory(product);
    }
}
