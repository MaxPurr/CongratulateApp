using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Person;
using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Options;
using CongratulateApi.Domain.Repositories.Interfaces;
using CongratulateApi.Utils.Helpers;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using CongratulateApi.DataAccess.Helpers;

namespace CongratulateApi.DataAccess.Repositories;

public class PersonRepository : PostgresRepository, IPersonRepository
{
    public PersonRepository(IOptions<PostgresOptions> postgresOptions) : base(postgresOptions.Value)
    { }

    public async Task<PersonEntity?> GetAsync(PersonGetModel personGet, CancellationToken token)
    {
        const string sqlQuery = @"
            select id
                 , name
                 , surname
                 , day_of_birth
                 , photo_url
              from people
             where id = @PersonId;
        ";
        var command = new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                PersonId = personGet.Id
            },
            cancellationToken: token);
        await using var connection = await GetConnectionAsync(token);
        var person = await connection.QuerySingleOrDefaultAsync<PersonEntity>(command);
        return person;
    }

    public async Task<ImageEntity?> GetPhotoAsync(PersonGetModel personGet, CancellationToken token)
    {
        const string sqlQuery = @"
           select photo_id as id
                , photo_url as url
             from people
            where id = @PersonId;
        ";
        var command = new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                PersonId = personGet.Id
            },
            cancellationToken: token);
        await using var connection = await GetConnectionAsync(token);
        var photo = await connection.QuerySingleOrDefaultAsync<ImageEntity>(command);
        return photo;
    }

    public async Task<PersonEntity?> SetPhotoAsync(PersonGetModel personGet, ImageEntity image, CancellationToken token)
    {
        const string sqlQuery = @"
              update people
                 set photo_id = @PhotoId
                   , photo_url = @PhotoUrl
               where id = @PersonId
           returning id, name, surname, day_of_birth, photo_url;
        ";
        var command = new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                PersonId = personGet.Id,
                PhotoId = image.Id,
                PhotoUrl = image.Url
            },
            cancellationToken: token);
        await using var connection = await GetConnectionAsync(token);
        var updatedPerson = await connection.QuerySingleOrDefaultAsync<PersonEntity>(command);
        return updatedPerson;
    }

    public async Task<List<PersonEntity>> GetAllAsync(PeopleOrderModel peopleOrder , CancellationToken token)
    {
        var command = PersonRepositoryHelper.CreateGetAllCommandDefinition(peopleOrder, token);
        await using var connection = await GetConnectionAsync(token);
        var people = await connection.QueryAsync<PersonEntity>(command);
        return people.ToList();
    }
    
    public async Task<List<BirthdayEntity>> GetBirthdaysAsync(BirthdaysGetModel birthdaysGet, BirthdaysOrderModel birthdaysOrder, CancellationToken token)
    {
        var command = PersonRepositoryHelper.CreateGetBirthdayCommandDefinition(birthdaysGet, birthdaysOrder, token);
        await using var connection = await GetConnectionAsync(token);
        var birthdays = await connection.QueryAsync<BirthdayEntity>(command);
        return birthdays.ToList();
    }

    public async Task<PersonEntity> AddAsync(PersonAddModel personAdd, CancellationToken token)
    {
        const string sqlQuery = @"
               insert into people (name, surname, day_of_birth)
               values (@Name, @Surname, @DayOfBirth)
            returning id, name, surname, day_of_birth, photo_url;
        ";
        var command = new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                personAdd.Name,
                personAdd.Surname,
                personAdd.DayOfBirth
            },
            cancellationToken: token);
        await using var connection = await GetConnectionAsync(token);
        var person = await connection.QuerySingleAsync<PersonEntity>(command);
        return person;
    }

    public async Task<PersonEntity?> RemoveAsync(PersonRemoveModel personRemove, CancellationToken token)
    {
        var sqlQuery = @"
               delete from people
                where id = @PersonId
            returning id, name, surname, day_of_birth, photo_url; 
        ";
        var command = new CommandDefinition(
            commandText: sqlQuery,
            parameters: new
            {
                PersonId = personRemove.Id
            },
            cancellationToken: token);
        await using var connection = await GetConnectionAsync(token);
        var person = await connection.QuerySingleOrDefaultAsync<PersonEntity>(command);
        return person;
    }

    public async Task<PersonEntity?> UpdateAsync(PersonUpdateModel personUpdate, CancellationToken token)
    {
        var personGet = new PersonGetModel()
        {
            Id = personUpdate.Id
        };
        var person = await GetAsync(personGet, token);
        if (person == null)
        {
            return null;
        }
        if (!UpdatePersonModelHelper.HasPropertiesForUpdating(personUpdate))
        {
            return person;
        }
        var command = PersonRepositoryHelper.CreateUpdateCommandDefinition(personUpdate, token);
        await using var connection = await GetConnectionAsync(token);
        var updatedPerson = await connection.QuerySingleOrDefaultAsync<PersonEntity>(command);
        return updatedPerson;
    }
}