using IWantApp.Domain.Orders;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Orders;

public class OrderGet
{
    public static string Template => "/orders/{id}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize]
    public static async Task<IResult> Action(Guid Id, HttpContext http, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        var clientId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeCode = http.User.Claims.FirstOrDefault(c => c.Type == "EmployeeCode");

        var order = context.Orders.Include(o => o.Products).FirstOrDefault(o => o.Id == Id);

        if (order.ClientId != clientId.Value && employeeCode == null)
        {
            return Results.Forbid();
        }

        var client = await userManager.FindByIdAsync(order.ClientId);

        var productsResponse = order.Products.Select(p => new OrderProduct(p.Id,p.Name));
        var orderResponse = new OrderResponse(order.Id,client.Email,productsResponse, order.DeliveryAddress);

        return Results.Ok(orderResponse);
    }
}
