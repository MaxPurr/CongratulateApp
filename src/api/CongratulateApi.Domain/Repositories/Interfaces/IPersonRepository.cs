using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Birthday;
using CongratulateApi.Domain.Models.Person;

namespace CongratulateApi.Domain.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<PersonEntity?> GetAsync(PersonGetModel personGet, CancellationToken token);
    Task<ImageEntity?> GetPhotoAsync(PersonGetModel personGet, CancellationToken token);
    Task<PersonEntity?> SetPhotoAsync(PersonGetModel personGet, ImageEntity photo, CancellationToken token);
    Task<List<PersonEntity>> GetAllAsync(PeopleOrderModel peopleOrder, CancellationToken token);
    Task<List<BirthdayEntity>> GetBirthdaysAsync(BirthdaysGetModel birthdaysGet, BirthdaysOrderModel order, CancellationToken token);
    Task<PersonEntity> AddAsync(PersonAddModel personAdd, CancellationToken token);
    Task<PersonEntity?> RemoveAsync(PersonRemoveModel personRemove, CancellationToken token);
    Task<PersonEntity?> UpdateAsync(PersonUpdateModel personUpdate, CancellationToken token);
}