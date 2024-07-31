using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Image;

namespace CongratulateApi.Domain.Repositories.Interfaces;

public interface IImageRepository
{
    Task<ImageEntity?> UploadAsync(ImageUploadModel imageUpload, CancellationToken token);
    Task DeleteAsync(ImageDeleteModel imageDelete, CancellationToken token);
}