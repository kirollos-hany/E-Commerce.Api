namespace E_Commerce.Api.Services;

public class ImageServices : IImageServices
{
    private readonly string _rootPath;

    private const string UploadDirectory = "UploadedImages";

    public ImageServices(IWebHostEnvironment env)
    {
        _rootPath = env.WebRootPath;
    }

    public void DeleteImage(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
        {
            return;
        }

        var path = Path.Combine(_rootPath, imagePath);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public async Task<string> SaveImageAsync(IFormFile file)
    {
        var fileInfo = new FileInfo(file.FileName);
        var fileName = string.Concat(Guid.NewGuid().ToString("N"), fileInfo.Extension);
        var fullPath = Path.Combine(_rootPath, UploadDirectory, fileName);
        await using (FileStream fs = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
        {
            await file.CopyToAsync(fs);
        }

        return Path.Combine(UploadDirectory, fileName);
    }

    public async Task<IEnumerable<string>> SaveImagesAsync(IFormFileCollection files)
    {
        IList<string> imagesPaths = new List<string>(files.Count);
        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file.FileName);
            var fileName = string.Concat(Guid.NewGuid().ToString("N"), fileInfo.Extension);
            var fullPath = Path.Combine(_rootPath, UploadDirectory, fileName);
            await using (FileStream fs = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                await file.CopyToAsync(fs);
            }
            imagesPaths.Add(Path.Combine(UploadDirectory, fileName));
        }

        return imagesPaths;
    }
}