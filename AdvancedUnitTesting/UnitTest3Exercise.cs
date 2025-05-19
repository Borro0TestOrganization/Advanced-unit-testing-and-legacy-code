namespace UnitTest3Exercise;

public enum Product
{
    Shampoo
}

public class Store
{
    private readonly Dictionary<Product, int> products = [];

    public void AddInventory(Product product, int quantity)
    {
        if (products.TryGetValue(product, out int value))
        {
            products[product] = value + quantity;
        }
        else
        {
            products[product] = quantity;
        }
    }

    public void RemoveInventory(Product product, int quantity)
    {
        products[product] = products[product] - quantity;
    }

    public int GetInventory(Product product)
    {
        return products[product];
    }
}

public class Customer
{
    public bool Purchase(Store store, Product product, int quantity)
    {
        if (store.GetInventory(product) > quantity)
        {
            store.RemoveInventory(product, quantity);
            return true;
        }

        return false;
    }
}


public class CustomerTests
{
    private Store store;
    private Customer sut;


    [SetUp]
    public void SetUp()
    {
        store = new Store();
        store.AddInventory(Product.Shampoo, 10);
        sut = new Customer();
    }

    [Test]
    public void Purchase_succeeds_when_enough_inventory()
    {
        bool success = sut.Purchase(store, Product.Shampoo, 5);

        Assert.That(success, Is.True);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(5));
    }
    [Test]
    public void Purchase_fails_when_not_enough_inventory()
    {
        bool success = sut.Purchase(store, Product.Shampoo, 15);

        Assert.That(success, Is.False);
        Assert.That(store.GetInventory(Product.Shampoo), Is.EqualTo(10));
    }
}
