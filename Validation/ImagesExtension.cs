using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.Validation;

public class ImagesExtension : ImageExtension
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFileCollection imagesFiles) return ValidationResult.Success;
        foreach (var imageFile in imagesFiles)
        {
            var result = ValidateImgExtension(imageFile);
            if (!result.Succeeded) return new ValidationResult(result.ToString());
        }
        return ValidationResult.Success;
    }
}