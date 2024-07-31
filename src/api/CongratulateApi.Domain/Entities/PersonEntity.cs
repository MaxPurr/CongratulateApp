namespace CongratulateApi.Domain.Entities;

public class PersonEntity
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required DateOnly DayOfBirth { get; init; }
    public required string PhotoUrl { get; init; }
}   