namespace CongratulateApi.Domain.Models.Birthday;

public class BirthdaysOrderModel
{
    public enum OrderBy
    {
        PersonId = 1,
        PersonName = 2,
        PersonSurname = 3,
        PersonAge = 4,
        DaysLeft = 5,
    }
    public OrderBy By { get; init; }
    public bool Descending { get; init; }
}