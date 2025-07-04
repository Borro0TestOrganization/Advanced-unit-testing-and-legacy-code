// Change vscode color theme to Light+ to get a nice color scheme to copy into slides.
// Paste into powerpoint by making a new textbox, paste with keep source formatting, change font size to 14.
// In Home -> Paragraph -> Line Spacing you can correct the line spacing if needed.



namespace AdvancedUnitTesting.AAAPattern;

public class SimpleStore
{
    public void Purchase_succeeds_when_enough_inventory()
    {
        // Arrange
        var store = new Store();
        store.AddInventory(Product.Shampoo, 10);
        var customer = new Customer();

        // Act
        bool success = customer.Purchase(store, Product.Shampoo, 5);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(5));
    }

    public void Purchase_succeeds_when_enough_inventory_BAD()
    {
        // Arrange
        var store = new Store();
        store.AddInventory(Product.Shampoo, 10);
        var customer = new Customer();

        // Act
        bool success = customer.Purchase(store, Product.Shampoo, 5);
        store.RemoveInventory(success, Product.Shampoo, 5);

        // Assert
        Assert.That(success, Is.True);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(5));
    }
}

internal class Customer
{
    public Customer()
    {
    }

    internal bool Purchase(Store store, Product shampoo, int v)
    {
        throw new NotImplementedException();
    }
}

internal enum Product
{
    Shampoo
}

internal class Store
{
    public Store()
    {
    }

    internal void AddInventory(object shampoo, int v)
    {
        throw new NotImplementedException();
    }

    internal int GetInventory(Product shampoo)
    {
        throw new NotImplementedException();
    }

    internal void RemoveInventory(bool success, Product shampoo, int v)
    {
        throw new NotImplementedException();
    }
}
