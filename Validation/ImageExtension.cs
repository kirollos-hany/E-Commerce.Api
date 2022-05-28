using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.Validation;

public class ImageExtension : ValidationAttribute
{
    private readonly List<string> _imageExtensions;

    public ImageExtension()
    {
        _imageExtensions = new List<string>
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile imageFile) return ValidationResult.Success;
        var result = ValidateImgExtension(imageFile);
        return !result.Succeeded ? new ValidationResult(result.ToString()) : ValidationResult.Success;
    }

    protected Result ValidateImgExtension(IFormFile file)
    {
        FileInfo fileInfo = new(file.FileName);
        if (_imageExtensions.Contains(fileInfo.Extension.ToLower())) return Result.Success;
        var supportedExtensions =
            _imageExtensions.Aggregate(string.Empty, (current, ext) => string.Concat(current, ext, "\n"));
        return Result.Failed(new ResultError()
            { Description = $"Image extension not supported.\nSupported extensions:\n{supportedExtensions}" });
    }
}