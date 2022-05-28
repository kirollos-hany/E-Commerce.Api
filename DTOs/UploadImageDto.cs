using System.ComponentModel.DataAnnotations;
using E_Commerce.Api.Validation;

namespace E_Commerce.Api.DTOs;

public class UploadImageDto
{
    [Required(ErrorMessage = "Image is required")]
    [ImageExtension]
    public IFormFile? ImageFile { get; set; }
}