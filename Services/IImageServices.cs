namespace E_Commerce.Api.Services;

public interface IImageServices
{
    Task<string> SaveImageAsync(IFormFile file);

    void DeleteImage(string imagePath);

    Task<IEnumerable<string>> SaveImagesAsync(IFormFileCollection files);
}