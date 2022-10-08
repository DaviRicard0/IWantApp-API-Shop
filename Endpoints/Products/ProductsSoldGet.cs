namespace IWantApp.Endpoints.Products;

public class ProductsSoldGet
{
    public static string Template => "/sold";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]
    public static async Task<IResult> Action(QueryAllProductsSold query)
    {
        var result = await query.Execute();
        return Results.Ok(result);
    }
}
