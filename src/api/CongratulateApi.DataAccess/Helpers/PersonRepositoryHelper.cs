using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Person;
using Dapper;

namespace CongratulateApi.DataAccess.Helpers;

public static class PersonRepositoryHelper
{
    private static string GetOrderByString(string columnName, bool orderByDescending)
    {
        return $"order by {columnName} {(orderByDescending ? "desc" : "asc")}"; 
    }
    
    private static string GetPersonOrderByString(PeopleOrderModel peopleOrder)
    {
        var columnName = peopleOrder.By switch
        {
            PeopleOrderModel.OrderBy.Name => "name",
            PeopleOrderModel.OrderBy.Surname => "surname",
            PeopleOrderModel.OrderBy.DayOfBirth => "day_of_birth", 
            _ => "id"
        };
        return GetOrderByString(columnName, peopleOrder.Descending);
    }

    public static CommandDefinition CreateGetAllCommandDefinition(PeopleOrderModel peopleOrder, CancellationToken token)
    {
        var orderBy = GetPersonOrderByString(peopleOrder);
        var sqlQuery = @$"
            select id
                 , name
                 , surname
                 , day_of_birth
                 , photo_url
              from people
            {orderBy}
        ";
        return new CommandDefinition(
            commandText: sqlQuery,
            cancellationToken: token);
    }
    
    private static void AppendSetterIfValueNotNull(List<string> setters, DynamicParameters parameters, string parameterName, object? value)
    {
        if (value == null)
        {
            return;
        }
        setters.Add($"{parameterName}=@{parameterName}");
        parameters.Add(parameterName, value);
    }

    public static CommandDefinition CreateUpdateCommandDefinition(PersonUpdateModel personUpdate, CancellationToken token)
    {
        var parameters = new DynamicParameters();
        parameters.Add("Id", personUpdate.Id);
        var setters = new List<string>();
        AppendSetterIfValueNotNull(setters, parameters, "name", personUpdate.Name);
        AppendSetterIfValueNotNull(setters, parameters, "surname", personUpdate.Surname);
        AppendSetterIfValueNotNull(setters, parameters, "day_of_birth", personUpdate.DayOfBirth);
        var sqlQuery = @$"
               update people
                  set {string.Join(", ", setters)}
                where id = @Id
            returning id, name, surname, day_of_birth, photo_url; 
        ";
        return new CommandDefinition(
            commandText: sqlQuery,
            parameters,
            cancellationToken: token);
    }
    
    private static string GetBirthdayOrderByString(BirthdaysOrderModel birthdaysOrder)
    {
        var columnName = birthdaysOrder.By switch
        {
            BirthdaysOrderModel.OrderBy.PersonName => "person_name",
            BirthdaysOrderModel.OrderBy.PersonSurname => "person_surname",
            BirthdaysOrderModel.OrderBy.PersonAge => "person_age", 
            BirthdaysOrderModel.OrderBy.DaysLeft => "days_left",
            _ => "person_id"
        };
        return GetOrderByString(columnName, birthdaysOrder.Descending);
    }
    
    public static CommandDefinition CreateGetBirthdayCommandDefinition(BirthdaysGetModel birthdaysGet, BirthdaysOrderModel birthdaysOrder,
        CancellationToken token)
    {
        var orderBy = GetBirthdayOrderByString(birthdaysOrder);
        var sqlQuery = @$"
            select p.id as person_id
                 , p.name as person_name
                 , p.surname as person_surname
                 , p.photo_url as person_photo_url
                 , (extract(year from p.next_birthday) - extract(year from p.day_of_birth)) as person_age
                 , p.next_birthday as date
                 , (p.next_birthday - @FromDate) as days_left
              from (select id
                         , name
                         , surname
                         , day_of_birth
                         , (case when (day_of_birth + interval '1 year' * (extract(year from @FromDate) - extract(year from day_of_birth))) >= @FromDate
                                 then (day_of_birth + interval '1 year' * (extract(year from @FromDate) - extract(year from day_of_birth)))
                               else (day_of_birth + interval '1 year' * (extract(year from @FromDate) - extract(year from day_of_birth) + 1))
                           end)::date as next_birthday
                         , photo_url
                      from people) p
             where (p.next_birthday - @FromDate) <= @MaxDaysLeft
            {orderBy}
        ";
        return new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                birthdaysGet.FromDate,
                birthdaysGet.MaxDaysLeft
            },
            cancellationToken: token);
    }
}