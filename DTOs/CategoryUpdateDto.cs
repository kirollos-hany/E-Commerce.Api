using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs;

public class CategoryUpdateDto
{
    [Required(ErrorMessage = "Category id is required")]
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category name is required")]
    [MaxLength(100, ErrorMessage = "Category name can't exceed 100 characters")]
    [MinLength(3, ErrorMessage = "Category name can't be less than 3 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category image id is required")]
    public string ImageId { get; set; } = string.Empty;
}