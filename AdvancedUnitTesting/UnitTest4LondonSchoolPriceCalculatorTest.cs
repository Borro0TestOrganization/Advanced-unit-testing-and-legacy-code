using NUnit.Framework;
using System;

namespace UnitTest4AdvancedCustomerStore;

[TestFixture]
public class PriceCalculatorTests
{
    [TestCase(Product.Shampoo, MembershipType.Regular, 10.00)]
    [TestCase(Product.Shampoo, MembershipType.Premium, 9.00)]
    [TestCase(Product.Book, MembershipType.Regular, 20.00)]
    [TestCase(Product.Book, MembershipType.Premium, 18.00)]
    public void CalculatePrice_ShouldReturnCorrectPriceForProductAndMembership(Product product, MembershipType membershipType, double expectedPrice)
    {
        // Arrange
        var priceCalculator = new PriceCalculator();

        // Act
        var actualPrice = priceCalculator.CalculatePrice(product, membershipType);

        // Assert
        Assert.That(actualPrice, Is.EqualTo(expectedPrice));
    }

    [Test]
    public void CalculatePrice_ShouldThrowArgumentExceptionForUnknownProduct()
    {
        // Arrange
        var priceCalculator = new PriceCalculator();
        // Cast an invalid integer to Product to simulate an unknown product
        var unknownProduct = (Product)99;

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => priceCalculator.CalculatePrice(unknownProduct, MembershipType.Regular));
        Assert.That(ex.Message, Is.EqualTo($"Unknown product: {unknownProduct}"));
    }
}
