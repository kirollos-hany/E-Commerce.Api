using System.ComponentModel.DataAnnotations;
using E_Commerce.Api.DataLayer.Models;

namespace E_Commerce.Api.DTOs;

public class ProductUpdateDto
{
    [Required(ErrorMessage = "Product id is required")]
    public string Id { get; set; } = string.Empty;

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

    [Required(ErrorMessage = "Product images is required")]

    public IEnumerable<ImageFile> Images { get; set; } = new List<ImageFile>();
}