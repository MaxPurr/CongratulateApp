using CongratulateApi.Domain.Providers.Interfaces;

namespace CongratulateApi.Utils.Providers;

public class DateOnlyProvider : IDateOnlyProvider
{
    public DateOnly CurrentDate => DateOnly.FromDateTime(DateTime.Now);
}