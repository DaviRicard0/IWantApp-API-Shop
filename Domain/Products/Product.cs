using IWantApp.Domain.Orders;

namespace IWantApp.Domain.Products;

public class Product : Entity
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public string Description {get;set; }
    public bool HasStock { get; set; }
    public bool Active { get; set; } = true;
    public decimal Price { get; private set; }
    public ICollection<Order> Orders { get; private set; }

    private Product() { }

    public Product(string name, Category category, string description, bool hasStock,decimal price, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();
    }
    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsGreaterOrEqualsThan(Price, 1, "Price")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }
}
