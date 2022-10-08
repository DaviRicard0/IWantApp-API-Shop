namespace IWantApp.Infra.Data;

public class QueryAllProductsSold
{
    private readonly IConfiguration configuration;

    public QueryAllProductsSold(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<ProductsSoldResponse>> Execute()
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query =
                @"SELECT p.Id,p.Name,count(*) Amount
                from Orders o inner join OrderProducts op on o.Id = op.OrdersId
                              inner join Products p on p.Id = op.ProductsId
                group BY p.Id, p.Name order by Amount desc";
        return await db.QueryAsync<ProductsSoldResponse>(query);
    }
}
