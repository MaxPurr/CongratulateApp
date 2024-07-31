using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CongratulateApi.Domain.Entities;
using CongratulateApi.Domain.Models.Image;
using CongratulateApi.Domain.Options;
using CongratulateApi.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Options;

namespace CongratulateApi.DataAccess.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly Cloudinary _cloudinary;
    
    public ImageRepository(IOptions<CloudinaryOptions> options)
    {
        var account = new Account(options.Value.CloudName, options.Value.ApiKey, options.Value.ApiSecret);
        _cloudinary = new Cloudinary(account);
    }
    
    public async Task<ImageEntity?> UploadAsync(ImageUploadModel imageUpload, CancellationToken token)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageUpload.FileName, imageUpload.FileStream)
        };
        var uploadResult = await _cloudinary.UploadAsync(uploadParams, token);
        if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return null;
        }
        return new ImageEntity()
        {
            Id = uploadResult.PublicId,
            Url = uploadResult.SecureUrl.ToString()
        };
    }

    public async Task DeleteAsync(ImageDeleteModel imageDelete, CancellationToken token)
    {
        var deletionParams = new DeletionParams(imageDelete.Id);
        var deletionResult = await _cloudinary.DestroyAsync(deletionParams);
        Console.WriteLine("---");
        Console.WriteLine(deletionResult.StatusCode);
        Console.WriteLine(deletionResult.Error);
        Console.WriteLine("---");
    }
}