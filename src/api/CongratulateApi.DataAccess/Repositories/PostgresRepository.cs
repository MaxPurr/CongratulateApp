using System.Transactions;
using Npgsql;
using CongratulateApi.Domain.Options;

namespace CongratulateApi.DataAccess.Repositories;

public abstract class PostgresRepository
{
    private readonly NpgsqlDataSource _dataSource;
    protected PostgresRepository(PostgresOptions postgresOptions, 
        Action<NpgsqlDataSourceBuilder>? configureDataSourceBuilder = null)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresOptions.ConnectionString);
        configureDataSourceBuilder?.Invoke(dataSourceBuilder);
        _dataSource = dataSourceBuilder.Build();
    }
    
    protected async Task<NpgsqlConnection> GetConnectionAsync(CancellationToken token)
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
        }
        var connection = await _dataSource.OpenConnectionAsync(token);
        await connection.ReloadTypesAsync();
        return connection;
    }
}