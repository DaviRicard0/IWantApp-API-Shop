namespace IWantApp.Domain.Orders;

public class Order : Entity
{
    public string ClientId { get; set; }
    public List<Product> Products { get; set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }

    private Order(){}

    public Order(string Id, string clientName, List<Product> products, string deliveryAddress)
    {
        ClientId = Id;
        Products = products;
        DeliveryAddress = deliveryAddress;
        Name = clientName;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;

        Total = 0;

        foreach (var item in products)
        {
            Total += item.Price;
        }

        Validate();
    }
    private void Validate()
    {
        var contract = new Contract<Order>()
            .IsNotNull(ClientId, "ClientId")
            .IsTrue(Products != null && Products.Any(),"Products")
            .IsNotNullOrEmpty(DeliveryAddress, "DeliveryAddress");
        AddNotifications(contract);
    }
}
