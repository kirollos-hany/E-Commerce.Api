using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Api.DTOs;

public class ProductCreateDto
{
    [Required(ErrorMessage = "Category id is required")]
    public string CategoryId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product name is required")]
    [MinLength(3, ErrorMessage = "Product name can't be less than 3 characters")]
    [MaxLength(100, ErrorMessage = "Product name can't exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product description is required")]
    [MinLength(3, ErrorMessage = "Product description can't be less than 3 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Product price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Product quantity in stock is required")]
    public uint QuantityInStock { get; set; }

    public IEnumerable<string> ImagesIds { get; set; } = new List<string>();
}