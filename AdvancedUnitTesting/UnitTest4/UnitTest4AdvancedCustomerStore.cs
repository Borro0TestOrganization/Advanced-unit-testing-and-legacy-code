namespace AdvancedUnitTesting.UnitTest4;

public interface IStore
{
    bool HasEnoughInventory(Product product, int quantity);
    void RemoveInventory(Product product, int quantity);
    void AddInventory(Product product, int quantity);
    int GetInventory(Product product);
}

public class Store : IStore
{
    private readonly Dictionary<Product, int> _inventory = new Dictionary<Product, int>();

    public bool HasEnoughInventory(Product product, int quantity)
    {
        return GetInventory(product) >= quantity;
    }

    public void RemoveInventory(Product product, int quantity)
    {
        if (!HasEnoughInventory(product, quantity))
        {
            throw new Exception("Not enough inventory");
        }

        _inventory[product] -= quantity;
    }

    public void AddInventory(Product product, int quantity)
    {
        if (_inventory.ContainsKey(product))
        {
            _inventory[product] += quantity;
        }
        else
        {
            _inventory.Add(product, quantity);
        }
    }

    public int GetInventory(Product product)
    {
        bool productExists = _inventory.TryGetValue(product, out int remaining);
        return productExists ? remaining : 0;
    }
}

public enum Product
{
    Shampoo,
    Book
}


public interface ICustomer
{
    bool Purchase(Product product, int quantity);
}

public class Customer : ICustomer
{
    private readonly IStore _store;

    public Customer(IStore store)
    {
        _store = store;
    }

    public bool Purchase(Product product, int quantity)
    {
        if (!_store.HasEnoughInventory(product, quantity))
        {
            return false;
        }


        try
        {
            _store.RemoveInventory(product, quantity);
            return true;
        }
        catch (InvalidOperationException)
        {
            // Revert inventory removal if bank withdrawal fails (important for atomicity in a real system)
            // In this simplified example, we're not implementing full transaction management.
            return false;
        }
    }
}
