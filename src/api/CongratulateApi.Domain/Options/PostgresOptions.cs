namespace CongratulateApi.Domain.Options;

public class PostgresOptions
{
    public required string ConnectionString { get; init; } = string.Empty;
}