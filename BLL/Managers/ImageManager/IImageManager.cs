
namespace Ecommerce.BLL
{
    public interface IImageManager
    {
        Task<GeneralResult<ImageUploadResultDto>> UploadAsync(ImageUploadDto dto,
            string basePath,
            string? schema,
            string? hostName,
            string folderName = "Images");
    }
}