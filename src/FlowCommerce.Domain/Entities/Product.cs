using FlowCommerce.Domain.Exceptions;

namespace FlowCommerce.Domain.Entities;

public sealed class Product
{
    public Product(string name, string sku, decimal price, int stockQuantity)
    {
        Name = ValidateRequired(name, "Product name is required.");
        Sku = ValidateRequired(sku, "Product SKU is required.");
        Price = ValidatePrice(price);

        if (stockQuantity < 0)
        {
            throw new DomainException("Stock quantity cannot be negative.");
        }

        Id = Guid.NewGuid();
        StockQuantity = stockQuantity;
        IsActive = true;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public Guid Id { get; }

    public string Name { get; private set; }

    public string Sku { get; }

    public decimal Price { get; private set; }

    public int StockQuantity { get; private set; }

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; }

    public DateTimeOffset UpdatedAt { get; private set; }

    public void ChangeName(string name)
    {
        Name = ValidateRequired(name, "Product name is required.");
        Touch();
    }

    public void ChangePrice(decimal price)
    {
        Price = ValidatePrice(price);
        Touch();
    }

    public void AddStock(int quantity)
    {
        ValidatePositiveQuantity(quantity, "Quantity to add must be greater than zero.");

        StockQuantity = checked(StockQuantity + quantity);
        Touch();
    }

    public void RemoveStock(int quantity)
    {
        ValidatePositiveQuantity(quantity, "Quantity to remove must be greater than zero.");

        if (quantity > StockQuantity)
        {
            throw new DomainException("Quantity to remove cannot exceed available stock.");
        }

        StockQuantity -= quantity;
        Touch();
    }

    public void Activate()
    {
        if (IsActive)
        {
            return;
        }

        IsActive = true;
        Touch();
    }

    public void Deactivate()
    {
        if (!IsActive)
        {
            return;
        }

        IsActive = false;
        Touch();
    }

    private static string ValidateRequired(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(message);
        }

        return value.Trim();
    }

    private static decimal ValidatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new DomainException("Product price must be greater than zero.");
        }

        return price;
    }

    private static void ValidatePositiveQuantity(int quantity, string message)
    {
        if (quantity <= 0)
        {
            throw new DomainException(message);
        }
    }

    private void Touch()
    {
        var now = DateTimeOffset.UtcNow;
        UpdatedAt = now > UpdatedAt ? now : UpdatedAt.AddTicks(1);
    }
}
