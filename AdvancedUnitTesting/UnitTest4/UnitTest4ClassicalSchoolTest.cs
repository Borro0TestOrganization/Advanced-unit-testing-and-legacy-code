namespace AdvancedUnitTesting.UnitTest4;

[TestFixture]
public class CustomerClassicalHardcodedTests
{
    // --- Scenario 1: Successful Purchase (Regular Member, Shampoo) ---
    [Test]
    public void Purchase_RegularMemberShampoo_EnoughFundsAndInventory_ShouldSucceed()
    {
        // Arrange
        var product = Product.Shampoo;
        var quantity = 1;
        var membershipType = MembershipType.Regular;

        // Hard-coded expected price for 1 Shampoo (Regular): 10.00
        var expectedTotalPrice = 10.00;
        var initialBalance = 100.00; // Enough funds
        var bankAccount = new BankAccount(initialBalance);

        var store = new Store();
        store.AddInventory(product, 5); // Enough inventory
        var initialStoreInventory = store.GetInventory(product);

        var priceCalculator = new PriceCalculator(); // Real calculator

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.True, "Purchase should succeed.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory - quantity), "Store inventory should decrease by 1.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance - expectedTotalPrice), "Bank account balance should decrease by 10.00.");
    }

    // --- Scenario 2: Successful Purchase (Premium Member, Book) ---
    [Test]
    public void Purchase_PremiumMemberBook_EnoughFundsAndInventory_ShouldSucceed()
    {
        // Arrange
        var product = Product.Book;
        var quantity = 2;
        var membershipType = MembershipType.Premium;

        // Hard-coded expected price for 2 Books (Premium): 18.00 * 2 = 36.00
        var expectedTotalPrice = 36.00;
        var initialBalance = 50.00; // Enough funds
        var bankAccount = new BankAccount(initialBalance);

        var store = new Store();
        store.AddInventory(product, 5); // Enough inventory
        var initialStoreInventory = store.GetInventory(product);

        var priceCalculator = new PriceCalculator(); // Real calculator

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.True, "Purchase should succeed.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory - quantity), "Store inventory should decrease by 2.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance - expectedTotalPrice), "Bank account balance should decrease by 36.00.");
    }

    // --- Scenario 3: Not Enough Inventory ---
    [Test]
    public void Purchase_NotEnoughInventory_ShouldReturnFalseAndNoChanges()
    {
        // Arrange
        var product = Product.Shampoo;
        var quantity = 5;
        var membershipType = MembershipType.Regular;

        var initialBalance = 100.00; // Assume funds are sufficient if inventory were there
        var bankAccount = new BankAccount(initialBalance);

        var store = new Store();
        store.AddInventory(product, 3); // Not enough inventory (need 5, only have 3)
        var initialStoreInventory = store.GetInventory(product);

        var priceCalculator = new PriceCalculator();

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.False, "Purchase should fail due to insufficient inventory.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory), "Store inventory should remain unchanged.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance), "Bank account balance should remain unchanged.");
    }

    // --- Scenario 4: Insufficient Funds ---
    [Test]
    public void Purchase_InsufficientFunds_ShouldReturnFalseAndNoChanges()
    {
        // Arrange
        var product = Product.Book;
        var quantity = 1;
        var membershipType = MembershipType.Regular;

        // Hard-coded expected price for 1 Book (Regular): 20.00
        var expectedTotalPrice = 20.00;
        var initialBalance = 15.00; // Insufficient funds (need 20.00, only have 15.00)
        var bankAccount = new BankAccount(initialBalance);

        var store = new Store();
        store.AddInventory(product, 5); // Enough inventory
        var initialStoreInventory = store.GetInventory(product);

        var priceCalculator = new PriceCalculator();

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.False, "Purchase should fail due to insufficient funds.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance), "Bank account balance should remain unchanged.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory), "Store inventory should remain unchanged (no rollback in current simplified code).");
    }

    // --- Scenario 5: Purchase Zero Quantity ---
    [Test]
    public void Purchase_ZeroQuantity_ShouldSucceedAndNotChangeState()
    {
        // Arrange
        var product = Product.Shampoo;
        var quantity = 0; // Zero quantity
        var membershipType = MembershipType.Regular;

        var store = new Store();
        store.AddInventory(product, 5);
        var initialStoreInventory = store.GetInventory(product);

        var initialBalance = 100.00;
        var bankAccount = new BankAccount(initialBalance);

        var priceCalculator = new PriceCalculator();

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType);

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.True, "Purchasing 0 quantity should return true.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory), "Store inventory should not change for 0 quantity.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance), "Bank account balance should not change for 0 quantity.");
    }

    // --- Scenario 6: Customer with no membership type (default behavior) ---
    [Test]
    public void Purchase_DefaultMembership_EnoughFundsAndInventory_ShouldSucceed()
    {
        // Arrange
        var product = Product.Shampoo;
        var quantity = 1;
        var membershipType = MembershipType.Regular; // Default will be Regular for non-premium pricing

        // Hard-coded expected price for 1 Shampoo (Regular): 10.00
        var expectedTotalPrice = 10.00;
        var initialBalance = 100.00;
        var bankAccount = new BankAccount(initialBalance);

        var store = new Store();
        store.AddInventory(product, 5);
        var initialStoreInventory = store.GetInventory(product);

        var priceCalculator = new PriceCalculator();

        var customer = new Customer(store, bankAccount, priceCalculator, membershipType); // Explicitly Regular

        // Act
        var result = customer.Purchase(product, quantity);

        // Assert
        Assert.That(result, Is.True, "Purchase should succeed.");
        Assert.That(store.GetInventory(product), Is.EqualTo(initialStoreInventory - quantity), "Store inventory should decrease.");
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance - expectedTotalPrice), "Bank account balance should decrease.");
    }
}
