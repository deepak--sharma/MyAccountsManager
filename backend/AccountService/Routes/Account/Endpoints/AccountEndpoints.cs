using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Models = AccountService.Models;

namespace AccountService.Routes.Account.Endpoints
{
    public static class AccountEndpoints
    {
        public static RouteGroupBuilder MapAccountEndpoints(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGetAccounts();
            groupBuilder.MapCreateAccount();
            groupBuilder.MapUpdateAccount();
            groupBuilder.MapDeleteAccount();
            return groupBuilder;
        }
        public static void MapGetAccounts(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/", async (AccountContext db) =>
            {
                var allAccounts = await db.Accounts.ToListAsync();
                return Results.Ok(allAccounts);
            })
            .WithName("GetAccounts");
        }


        public static void MapCreateAccount(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/add", async (Models.Account account, AccountContext db) =>
            {
                db.Accounts.Add(account);
                await db.SaveChangesAsync();
                return Results.Created($"/api/account/{account.Id}", account);
            })
            .WithName("CreateAccount");
        }

        public static void MapUpdateAccount(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPut("/update/{id}", async (HttpContext context, Models.Account account, AccountContext db) =>
            {
                var id = context.Request.RouteValues["id"];

                var dbAccount = await db.Accounts.FindAsync(Convert.ToInt32(id));
                if (dbAccount == null) return Results.NotFound("{ \"error\": \"Account not found\" }");

                dbAccount.FirstName = account.FirstName;
                dbAccount.LastName = account.LastName;

                await db.SaveChangesAsync();
                return Results.Ok(dbAccount);
            })
            .WithName("UpdateAccount");
        }

        public static void MapDeleteAccount(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapDelete("/delete/{id}", async (HttpContext context, AccountContext db) =>
            {
                var id = context.Request.RouteValues["id"];
                // Console.WriteLine($"Deleting account with ID = {id}");

                var dbAccount = await db.Accounts.FindAsync(Convert.ToInt32(id));
                if (dbAccount == null) return Results.NotFound("{ \"error\": \"Account not found\" }");

                db.Accounts.Remove(dbAccount);
                await db.SaveChangesAsync();
                return Results.Ok();
            })
            .WithName("DeleteAccount");
        }
    }
}