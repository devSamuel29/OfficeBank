using Domain.Account.Models;
using Domain.Transaction.Models;
using Domain.Transfer.Models;
using Infra.Shared.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infra.Shared.SqlServer.Context;

internal sealed class SqlServerWriteContext(
    DbContextOptions<SqlServerWriteContext> options,
    SqlServerConnectionString sqlServerConnection
) : DbContext(options)
{
    private readonly SqlServerConnectionString _sqlServerConnectionString =
        sqlServerConnection;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(SqlServerWriteContext).Assembly,
            WriteConfigurationsFilter
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        bool isTesting = false;

        if (isTesting)
        {
            optionsBuilder.UseInMemoryDatabase("");
        }
        else
        {
            optionsBuilder.UseSqlServer(_sqlServerConnectionString.ToString());
        }
    }

    private static bool WriteConfigurationsFilter(Type type) =>
        type.FullName?.Contains("Configurations.Write") ?? false;

    public DbSet<AccountModel> Accounts { get; set; }

    public DbSet<TransferModel> Transfers { get; set; }

    public DbSet<TransactionModel> Transactions { get; set; }
}
