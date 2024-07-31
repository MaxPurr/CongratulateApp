namespace CongratulateApi.Domain.Models.Person;

public class PeopleOrderModel
{
    public enum OrderBy
    {
        Id = 1,
        Name = 2,
        Surname = 3,
        DayOfBirth = 4,
    }
    public OrderBy By { get; init; } = OrderBy.Id;
    public bool Descending { get; init; } = false;
}