namespace IWantApp.Endpoints.Orders;

public record OrderRequest(List<Guid> ProductsIds, string DeliveryAddress);