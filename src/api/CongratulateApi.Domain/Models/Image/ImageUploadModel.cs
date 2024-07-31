namespace CongratulateApi.Domain.Models.Image;

public class ImageUploadModel
{
    public required string FileName { get; init; }
    public required Stream FileStream { get; init; }
}