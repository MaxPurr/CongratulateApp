namespace CongratulateApi.Domain.Models.Person;

public class PersonAddModel
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required DateOnly DayOfBirth { get; init; }
}