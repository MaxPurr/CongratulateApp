using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Person;
using CongratulateApi.Models;

namespace CongratulateApi.Extensions;

public static class ConversionExtensions
{
    public static PeopleOrderModel.OrderBy ToDomain(this PeopleOrderBy orderBy)
    {
        return orderBy switch
        {
            PeopleOrderBy.Name => PeopleOrderModel.OrderBy.Name,
            PeopleOrderBy.Surname => PeopleOrderModel.OrderBy.Surname,
            PeopleOrderBy.DayOfBirth => PeopleOrderModel.OrderBy.DayOfBirth,
            _ => PeopleOrderModel.OrderBy.Id
        };
    }

    public static BirthdaysOrderModel.OrderBy ToDomain(this BirthdaysOrderBy orderBy)
    {
        return orderBy switch
        {
            BirthdaysOrderBy.PersonName => BirthdaysOrderModel.OrderBy.PersonName,
            BirthdaysOrderBy.PersonSurname => BirthdaysOrderModel.OrderBy.PersonSurname,
            BirthdaysOrderBy.PersonAge => BirthdaysOrderModel.OrderBy.PersonAge,
            BirthdaysOrderBy.DaysLeft => BirthdaysOrderModel.OrderBy.DaysLeft,
            _ => BirthdaysOrderModel.OrderBy.PersonId
        };
    }
}