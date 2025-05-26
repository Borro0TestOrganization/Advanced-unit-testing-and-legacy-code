using UnitTest4AdvancedCustomerStore;

namespace UnitTest4LondonSchoolTest;

[TestFixture]
public class StoreTests
{
    [Test]
    public void AddInventory_ShouldIncreaseInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Shampoo;
        var quantity = 10;

        // Act
        store.AddInventory(product, quantity);

        // Assert
        Assert.That(store.GetInventory(product), Is.EqualTo(quantity));
    }

    [Test]
    public void AddInventory_ShouldAddMultipleTimesForSameProduct()
    {
        // Arrange
        var store = new Store();
        var product = Product.Book;
        var quantity1 = 5;
        var quantity2 = 3;

        // Act
        store.AddInventory(product, quantity1);
        store.AddInventory(product, quantity2);

        // Assert
        Assert.That(store.GetInventory(product), Is.EqualTo(quantity1 + quantity2));
    }

    [Test]
    public void GetInventory_ShouldReturnZeroForNonExistentProduct()
    {
        // Arrange
        var store = new Store();
        var product = Product.Book;

        // Act & Assert
        Assert.That(store.GetInventory(product), Is.EqualTo(0));
    }

    [Test]
    public void HasEnoughInventory_ShouldReturnTrueIfEnoughInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Shampoo;
        store.AddInventory(product, 5);

        // Act & Assert
        Assert.That(store.HasEnoughInventory(product, 3), Is.True);
    }

    [Test]
    public void HasEnoughInventory_ShouldReturnTrueIfExactlyEnoughInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Shampoo;
        store.AddInventory(product, 5);

        // Act & Assert
        Assert.That(store.HasEnoughInventory(product, 5), Is.True);
    }

    [Test]
    public void HasEnoughInventory_ShouldReturnFalseIfNotEnoughInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Shampoo;
        store.AddInventory(product, 5);

        // Act & Assert
        Assert.That(store.HasEnoughInventory(product, 7), Is.False);
    }

    [Test]
    public void RemoveInventory_ShouldDecreaseInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Book;
        store.AddInventory(product, 10);
        var quantityToRemove = 4;

        // Act
        store.RemoveInventory(product, quantityToRemove);

        // Assert
        Assert.That(store.GetInventory(product), Is.EqualTo(6));
    }

    [Test]
    public void RemoveInventory_ShouldThrowExceptionWhenNotEnoughInventory()
    {
        // Arrange
        var store = new Store();
        var product = Product.Book;
        store.AddInventory(product, 3);
        var quantityToRemove = 5;

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => store.RemoveInventory(product, quantityToRemove));
        Assert.That(ex.Message, Is.EqualTo("Not enough inventory"));
        Assert.That(store.GetInventory(product), Is.EqualTo(3)); // Ensure inventory remains unchanged
    }

    [Test]
    public void RemoveInventory_ShouldThrowExceptionWhenProductDoesNotExist()
    {
        // Arrange
        var store = new Store();
        var product = Product.Book;
        var quantityToRemove = 5;

        // Act & Assert
        var ex = Assert.Throws<Exception>(() => store.RemoveInventory(product, quantityToRemove));
        Assert.That(ex.Message, Is.EqualTo("Not enough inventory"));
        Assert.That(store.GetInventory(product), Is.EqualTo(0)); // Ensure inventory remains unchanged
    }
}
