using Microsoft.EntityFrameworkCore;
using Models = AccountService.Models;

namespace AccountService.Routes.Account.Endpoints
{
    public static class SearchAccountEndpoints
    {
        public static RouteGroupBuilder MapSearchAccountEndpoints(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapSearchAccount();
            return groupBuilder;
        }
        public static void MapSearchAccount(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/search", async (string? id, string? lastName, AccountContext db) =>
            {
                IQueryable<Models.Account> query = db.Accounts;
                if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int accountId))
                {
                    query = query.Where(a => a.Id == accountId);
                }
                else if (!string.IsNullOrEmpty(lastName))
                {
                    query = query.Where(a => EF.Functions.Like(a.LastName, $"{lastName}%"));
                }
                else
                {
                    return Results.BadRequest("Provide either an ID or Last Name.");
                }
                var results = await query.OrderBy(a => a.Id).ToListAsync();

                return Results.Ok(results);
            })
            .WithName("SearchAccount");
        }
    }
}