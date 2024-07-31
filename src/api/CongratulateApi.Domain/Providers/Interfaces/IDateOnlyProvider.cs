namespace CongratulateApi.Domain.Providers.Interfaces;

public interface IDateOnlyProvider
{
    DateOnly CurrentDate { get; }
}