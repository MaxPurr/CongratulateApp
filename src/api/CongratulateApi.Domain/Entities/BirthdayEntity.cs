namespace CongratulateApi.Domain.Entities;

public class BirthdayEntity
{
    public required long PersonId { get; init; }
    public required string PersonName { get; init; }
    public required string PersonSurname { get; init; }
    public required string PersonPhotoUrl { get; init; }
    public required int PersonAge { get; init; }
    public required DateOnly Date { get; init; }
    public required int DaysLeft { get; init; }
}