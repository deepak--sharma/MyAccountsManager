using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Routes.Account.Endpoints
{
    public static class AccountTradeEndpoints
    {
        public static RouteGroupBuilder MapTradeEndpoints(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGetAccountTrades();
            groupBuilder.MapCreateAccountTrade();
            groupBuilder.MapUpdateAccountTrade();
            return groupBuilder;
        }
        public static void MapCreateAccountTrade(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/add", async (Models.AccountTrade trade, AccountContext db, IValidator<Models.AccountTrade> validator) =>
            {
                var validationResult = await validator.ValidateAsync(trade);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                db.AccountTrades.Add(trade);
                await db.SaveChangesAsync();
                return Results.Created($"/api/trade/{trade.Id}", trade);
            })
            .WithName("CreateAccountTrade");
        }
        public static void MapGetAccountTrades(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/", async (AccountContext db) =>
            {
                var allAccountTrades = await db.AccountTrades.ToListAsync();
                return Results.Ok(allAccountTrades);
            })
            .WithName("GetAccountTrades");
        }
        public static void MapUpdateAccountTrade(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPut("/update/{id}", async (HttpContext context, Models.AccountTrade trade, AccountContext db, IValidator<Models.AccountTrade> validator) =>
            {
                var validationResult = await validator.ValidateAsync(trade);
                if (!validationResult.IsValid)
                {
                    return Results.BadRequest(validationResult.Errors);
                }

                var id = context.Request.RouteValues["id"];

                var dbTrade = await db.AccountTrades.FindAsync(Convert.ToInt32(id));
                if (dbTrade == null) return Results.NotFound("{ \"error\": \"Trade not found\" }");

                dbTrade.Status = trade.Status;

                await db.SaveChangesAsync();
                return Results.Ok(dbTrade);
            })
            .WithName("UpdateAccountTrade");
        }
    }
}