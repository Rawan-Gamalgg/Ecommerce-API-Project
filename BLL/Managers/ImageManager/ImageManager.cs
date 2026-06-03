
namespace Ecommerce.BLL
{
    public class ImageManager : IImageManager
    {//save to cloud or hard desk and return the url (no saving to database yet)
        public async Task<GeneralResult<ImageUploadResultDto>> UploadAsync(
            ImageUploadDto dto,
            string basePath,
            string? schema,
            string? hostName,
             string folderName = "Images")
        {
            if (string.IsNullOrWhiteSpace(schema) || string.IsNullOrWhiteSpace(hostName))
            {
                return GeneralResult<ImageUploadResultDto>.Fail("Missing schema or host name");
            }
            var file = dto.File;
            var extension = Path.GetExtension(file.FileName);
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName).
                Replace(" ", "-").ToLower();
            var newfileName = $"{nameWithoutExtension}-{Guid.NewGuid()}{extension}";
            //var folder = Path.GetDirectoryName(file.FileName);
            var directoryPath = Path.Combine(basePath.Replace(" ", "-"), "Files", folderName);//files/images
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fullPath = Path.Combine(directoryPath, newfileName);//files/images/product1.jpg
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);//save the file to the specified path
            }
            //schema/hostname/files/images/product1.jpg
            var imageUrl = $"{schema}://{hostName}/{directoryPath.Replace("\\", "/")}/{newfileName}";
            var result = new ImageUploadResultDto(imageUrl);
            return GeneralResult<ImageUploadResultDto>.Ok(result, "Image uploaded successfully");

        }

    }
}
