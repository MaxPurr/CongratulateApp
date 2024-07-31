namespace CongratulateApi.Domain.Models.Birthday;

public class BirthdaysGetModel
{
    public required DateOnly FromDate { get; init; }
    public required int MaxDaysLeft { get; init; }
}