using CustomerStore;

namespace UnitTest4Exercise;

public class RefactoringTestsInTwoSchools
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
