namespace AdvancedUnitTesting.UnitTest4;

[TestFixture]
public class BankAccountTests
{
    [Test]
    public void Constructor_ShouldSetInitialBalance()
    {
        // Arrange
        var initialBalance = 100.0;

        // Act
        var bankAccount = new BankAccount(initialBalance);

        // Assert
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(initialBalance));
    }

    [Test]
    public void Deposit_ShouldIncreaseCurrentBalance()
    {
        // Arrange
        var bankAccount = new BankAccount(50.0);
        var amountToDeposit = 25.0;

        // Act
        bankAccount.Deposit(amountToDeposit);

        // Assert
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(75.0));
    }

    [Test]
    public void Withdraw_ShouldDecreaseCurrentBalance()
    {
        // Arrange
        var bankAccount = new BankAccount(100.0);
        var amountToWithdraw = 30.0;

        // Act
        bankAccount.Withdraw(amountToWithdraw);

        // Assert
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(70.0));
    }

    [Test]
    public void Withdraw_ShouldThrowInvalidOperationExceptionWhenInsufficientFunds()
    {
        // Arrange
        var bankAccount = new BankAccount(20.0);
        var amountToWithdraw = 50.0;

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => bankAccount.Withdraw(amountToWithdraw));
        Assert.That(ex.Message, Is.EqualTo("Insufficient funds"));
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(20.0)); // Ensure balance remains unchanged
    }

    [Test]
    public void Withdraw_ShouldAllowWithdrawalOfExactBalance()
    {
        // Arrange
        var bankAccount = new BankAccount(50.0);
        var amountToWithdraw = 50.0;

        // Act
        bankAccount.Withdraw(amountToWithdraw);

        // Assert
        Assert.That(bankAccount.CurrentBalance, Is.EqualTo(0.0));
    }
}
