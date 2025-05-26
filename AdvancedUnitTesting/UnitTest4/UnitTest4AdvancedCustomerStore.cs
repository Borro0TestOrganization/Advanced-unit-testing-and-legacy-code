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

public interface IBankAccount
{
    double CurrentBalance { get; }
    void Deposit(double amount);
    void Withdraw(double amount);
}

public class BankAccount : IBankAccount
{
    public double CurrentBalance { get; private set; }

    public BankAccount(double initialBalance)
    {
        CurrentBalance = initialBalance;
    }

    public void Deposit(double amount)
    {
        CurrentBalance += amount;
    }

    public void Withdraw(double amount)
    {
        if (CurrentBalance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }
        CurrentBalance -= amount;
    }
}

public interface IPriceCalculator
{
    double CalculatePrice(Product product, MembershipType membershipType);
}

public class PriceCalculator : IPriceCalculator
{
    public double CalculatePrice(Product product, MembershipType membershipType)
    {
        switch (product)
        {
            case Product.Shampoo:
                return membershipType == MembershipType.Premium ? 9.00 : 10.00;
            case Product.Book:
                return membershipType == MembershipType.Premium ? 18.00 : 20.00;
            default:
                throw new ArgumentException($"Unknown product: {product}");
        }
    }
}

public enum MembershipType
{
    Regular,
    Premium
}

public interface ICustomer
{
    bool Purchase(Product product, int quantity);
}

public class Customer : ICustomer
{
    private readonly IStore _store;
    private readonly IBankAccount _bankAccount;
    private readonly IPriceCalculator _priceCalculator;
    private readonly MembershipType _membershipType;

    public Customer(IStore store, IBankAccount bankAccount, IPriceCalculator priceCalculator, MembershipType membershipType)
    {
        _store = store;
        _bankAccount = bankAccount;
        _priceCalculator = priceCalculator;
        _membershipType = membershipType;
    }

    public bool Purchase(Product product, int quantity)
    {
        if (!_store.HasEnoughInventory(product, quantity))
        {
            return false;
        }

        double price = _priceCalculator.CalculatePrice(product, _membershipType) * quantity;

        try
        {
            _bankAccount.Withdraw(price);
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
