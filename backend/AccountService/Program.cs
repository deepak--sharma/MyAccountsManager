using AccountService.Routes.Account.Endpoints;
using AccountService.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AccountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddValidatorsFromAssemblyContaining<AccountTradeValidator>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGroup("/api/account")
    .MapAccountEndpoints()
    .WithName("Account");


app.MapGroup("api/trade")
    .MapTradeEndpoints()
    .WithName("Trade");

app.MapGroup("/api/accounts")
    .MapSearchAccountEndpoints()
    .WithName("SearchAccount");

app.Run();