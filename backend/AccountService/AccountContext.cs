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


}