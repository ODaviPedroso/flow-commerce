using FlowCommerce.Domain.Entities;
using FlowCommerce.Domain.Exceptions;

namespace FlowCommerce.UnitTests.Domain.Entities;

public sealed class ProductTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesActiveProduct()
    {
        var beforeCreation = DateTimeOffset.UtcNow;

        var product = CreateProduct(stockQuantity: 8);

        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal("Mechanical Keyboard", product.Name);
        Assert.Equal("KEYBOARD-001", product.Sku);
        Assert.Equal(299.90m, product.Price);
        Assert.Equal(8, product.StockQuantity);
        Assert.True(product.IsActive);
        Assert.InRange(product.CreatedAt, beforeCreation, DateTimeOffset.UtcNow);
        Assert.Equal(product.CreatedAt, product.UpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidName_ThrowsDomainException(string name)
    {
        var exception = Assert.Throws<DomainException>(
            () => new Product(name, "KEYBOARD-001", 299.90m, 8));

        Assert.Equal("Product name is required.", exception.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_WithInvalidSku_ThrowsDomainException(string sku)
    {
        var exception = Assert.Throws<DomainException>(
            () => new Product("Mechanical Keyboard", sku, 299.90m, 8));

        Assert.Equal("Product SKU is required.", exception.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.01)]
    public void Constructor_WithInvalidPrice_ThrowsDomainException(decimal price)
    {
        var exception = Assert.Throws<DomainException>(
            () => new Product("Mechanical Keyboard", "KEYBOARD-001", price, 8));

        Assert.Equal("Product price must be greater than zero.", exception.Message);
    }

    [Fact]
    public void Constructor_WithNegativeStock_ThrowsDomainException()
    {
        var exception = Assert.Throws<DomainException>(
            () => CreateProduct(stockQuantity: -1));

        Assert.Equal("Stock quantity cannot be negative.", exception.Message);
    }

    [Fact]
    public void ChangeName_WithValidName_ChangesNameAndUpdatesTimestamp()
    {
        var product = CreateProduct();
        var previousUpdatedAt = product.UpdatedAt;

        product.ChangeName("Ergonomic Keyboard");

        Assert.Equal("Ergonomic Keyboard", product.Name);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangeName_WithInvalidName_ThrowsDomainException(string name)
    {
        var product = CreateProduct();

        var exception = Assert.Throws<DomainException>(() => product.ChangeName(name));

        Assert.Equal("Product name is required.", exception.Message);
    }

    [Fact]
    public void ChangePrice_WithValidPrice_ChangesPriceAndUpdatesTimestamp()
    {
        var product = CreateProduct();
        var previousUpdatedAt = product.UpdatedAt;

        product.ChangePrice(349.90m);

        Assert.Equal(349.90m, product.Price);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-0.01)]
    public void ChangePrice_WithInvalidPrice_ThrowsDomainException(decimal price)
    {
        var product = CreateProduct();

        var exception = Assert.Throws<DomainException>(() => product.ChangePrice(price));

        Assert.Equal("Product price must be greater than zero.", exception.Message);
    }

    [Fact]
    public void AddStock_WithValidQuantity_IncreasesStockAndUpdatesTimestamp()
    {
        var product = CreateProduct(stockQuantity: 8);
        var previousUpdatedAt = product.UpdatedAt;

        product.AddStock(4);

        Assert.Equal(12, product.StockQuantity);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void AddStock_WithInvalidQuantity_ThrowsDomainException(int quantity)
    {
        var product = CreateProduct();

        var exception = Assert.Throws<DomainException>(() => product.AddStock(quantity));

        Assert.Equal("Quantity to add must be greater than zero.", exception.Message);
    }

    [Fact]
    public void RemoveStock_WithAvailableQuantity_DecreasesStockAndUpdatesTimestamp()
    {
        var product = CreateProduct(stockQuantity: 8);
        var previousUpdatedAt = product.UpdatedAt;

        product.RemoveStock(3);

        Assert.Equal(5, product.StockQuantity);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void RemoveStock_WithInvalidQuantity_ThrowsDomainException(int quantity)
    {
        var product = CreateProduct();

        var exception = Assert.Throws<DomainException>(() => product.RemoveStock(quantity));

        Assert.Equal("Quantity to remove must be greater than zero.", exception.Message);
    }

    [Fact]
    public void RemoveStock_WithQuantityGreaterThanStock_ThrowsDomainException()
    {
        var product = CreateProduct(stockQuantity: 8);

        var exception = Assert.Throws<DomainException>(() => product.RemoveStock(9));

        Assert.Equal("Quantity to remove cannot exceed available stock.", exception.Message);
    }

    [Fact]
    public void Deactivate_WhenActive_DeactivatesAndUpdatesTimestamp()
    {
        var product = CreateProduct();
        var previousUpdatedAt = product.UpdatedAt;

        product.Deactivate();

        Assert.False(product.IsActive);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Fact]
    public void Activate_WhenInactive_ActivatesAndUpdatesTimestamp()
    {
        var product = CreateProduct();
        product.Deactivate();
        var previousUpdatedAt = product.UpdatedAt;

        product.Activate();

        Assert.True(product.IsActive);
        Assert.True(product.UpdatedAt > previousUpdatedAt);
    }

    [Fact]
    public void Activate_WhenAlreadyActive_DoesNotUpdateTimestamp()
    {
        var product = CreateProduct();
        var previousUpdatedAt = product.UpdatedAt;

        product.Activate();

        Assert.Equal(previousUpdatedAt, product.UpdatedAt);
    }

    [Fact]
    public void Deactivate_WhenAlreadyInactive_DoesNotUpdateTimestamp()
    {
        var product = CreateProduct();
        product.Deactivate();
        var previousUpdatedAt = product.UpdatedAt;

        product.Deactivate();

        Assert.Equal(previousUpdatedAt, product.UpdatedAt);
    }

    private static Product CreateProduct(int stockQuantity = 8)
    {
        return new Product("Mechanical Keyboard", "KEYBOARD-001", 299.90m, stockQuantity);
    }
}
