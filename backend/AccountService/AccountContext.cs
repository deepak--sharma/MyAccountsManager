using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class AccountContext : DbContext
{
    public AccountContext(DbContextOptions<AccountContext> options)
        : base(options)
    {

    }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTrade> AccountTrades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //One to many relationship between Account and AccountTrade
        modelBuilder.Entity<Account>()
        .HasMany(a => a.AccountTrades);

        //Index on LastName for searching faster
        modelBuilder.Entity<Account>()
        .HasIndex(a => a.LastName);
    }
}