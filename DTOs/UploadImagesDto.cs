using System.ComponentModel.DataAnnotations;
using E_Commerce.Api.Validation;

namespace E_Commerce.Api.DTOs;

public class UploadImagesDto
{
    [Required(ErrorMessage = "Images are required")]
    [ImagesExtension]
    [MinLength(1, ErrorMessage = "Images can't be empty")]
    public IFormFileCollection ImagesCollection { get; set; } = new FormFileCollection();
}