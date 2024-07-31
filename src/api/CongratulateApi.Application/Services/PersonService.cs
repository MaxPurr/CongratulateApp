using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Image;
using CongratulateApi.Domain.Models.Person;
using CongratulateApi.Domain.Providers.Interfaces;
using CongratulateApi.Domain.Repositories.Interfaces;
using CongratulateApi.Domain.Services.Interfaces;

namespace CongratulateApi.Application.Services;

public class PersonService : IPersonService
{
    private readonly IDateOnlyProvider _dateOnlyProvider;
    private readonly IPersonRepository _personRepository;
    private readonly IImageRepository _imageRepository;

    public PersonService(IDateOnlyProvider dateOnlyProvider, IPersonRepository personRepository, IImageRepository imageRepository)
    {
        _dateOnlyProvider = dateOnlyProvider;
        _personRepository = personRepository;
        _imageRepository = imageRepository;
    }

    public async Task<PersonEntity?> GetAsync(PersonGetModel personGet, CancellationToken token)
    {
        var person = await _personRepository.GetAsync(personGet, token);
        return person;
    }

    public async Task<List<PersonEntity>> GetAllAsync(PeopleOrderModel peopleOrder, CancellationToken token)
    {
        var people = await _personRepository.GetAllAsync(peopleOrder, token);
        return people;
    }
    
    public async Task<List<BirthdayEntity>> GetNearestBirthdaysAsync(int maxDaysLeft, BirthdaysOrderModel birthdaysOrder, 
        CancellationToken token)
    {
        var birthdaysGet = new BirthdaysGetModel()
        {
            FromDate = _dateOnlyProvider.CurrentDate,
            MaxDaysLeft = maxDaysLeft
        };
        var birthdays = await _personRepository.GetBirthdaysAsync(birthdaysGet, birthdaysOrder, token);
        return birthdays;
    }

    public async Task<PersonEntity> AddAsync(PersonAddModel personAdd, CancellationToken token)
    {
        var person = await _personRepository.AddAsync(personAdd, token);
        return person;
    }

    public async Task<PersonEntity?> RemoveAsync(PersonRemoveModel personRemove, CancellationToken token)
    {
        var person = await _personRepository.RemoveAsync(personRemove, token);
        return person;
    }

    public async Task<PersonEntity?> UpdateAsync(PersonUpdateModel personUpdate, CancellationToken token)
    {
        var updatedPerson = await _personRepository.UpdateAsync(personUpdate, token);
        return updatedPerson;
    }

    public async Task<PersonEntity?> UploadPhotoAsync(PersonGetModel personGet, ImageUploadModel imageUpload, CancellationToken token)
    {
        var previousPhoto = await _personRepository.GetPhotoAsync(personGet, token);
        if (previousPhoto != null && previousPhoto.Id != null)
        {
            var imageDelete = new ImageDeleteModel()
            {
                Id = previousPhoto.Id
            };
            await _imageRepository.DeleteAsync(imageDelete, token);
        }
        var newPhoto = await _imageRepository.UploadAsync(imageUpload, token);
        if (newPhoto == null)
        {
            return null;
        }
        var updatedPerson = await _personRepository.SetPhotoAsync(personGet, newPhoto, token);
        return updatedPerson;
    }
}