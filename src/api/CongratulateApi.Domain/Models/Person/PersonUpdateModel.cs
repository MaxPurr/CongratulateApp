namespace CongratulateApi.Domain.Models.Person;

public class PersonUpdateModel
{
    public required long Id { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public DateOnly? DayOfBirth { get; init; }
}