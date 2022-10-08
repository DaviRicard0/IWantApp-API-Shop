namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName queryAllUsers)
    {
        if (page == null)
        {
            return Results.BadRequest(new { error=$"Você informou para page no paramêtro o valor null! Coloque outro valor." });
        }

        if (rows == null)
        {
            return Results.BadRequest(new { error=$"Você informou para rows no paramêtro o valor null! Coloque outro valor." });
        }
        var result = await queryAllUsers.Execute(page.Value, rows.Value);
        return Results.Ok(result);

        //Paginação fórmula para pegar o número da página e a quantida de linhas desejadas, com entity
        /*var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var item in users)
        {
            var claims = userManager.GetClaimsAsync(item).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(item.Email, userName));
        }
        return Results.Ok(employees);*/
    }
}
