namespace CongratulateApi.Domain.Options;

public class CloudinaryOptions
{
    public required string CloudName { get; init; }
    public required string ApiKey { get; init; }
    public required string ApiSecret { get; init; }
}