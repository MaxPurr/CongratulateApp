using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Image;
using CongratulateApi.Domain.Models.Person;

namespace CongratulateApi.Domain.Services.Interfaces;

public interface IPersonService
{
    Task<PersonEntity?> GetAsync(PersonGetModel personGet, CancellationToken token);
    Task<List<PersonEntity>> GetAllAsync(PeopleOrderModel peopleOrder, CancellationToken token);
    Task<List<BirthdayEntity>> GetNearestBirthdaysAsync(int maxDaysLeft, BirthdaysOrderModel birthdaysOrder, CancellationToken token);
    Task<PersonEntity> AddAsync(PersonAddModel personAdd, CancellationToken token);
    Task<PersonEntity?> RemoveAsync(PersonRemoveModel removePerson, CancellationToken token);
    Task<PersonEntity?> UpdateAsync(PersonUpdateModel personUpdate, CancellationToken token);
    Task<PersonEntity?> UploadPhotoAsync(PersonGetModel personGet, ImageUploadModel imageUpload, CancellationToken token);
}