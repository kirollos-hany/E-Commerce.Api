using E_Commerce.Api.DataLayer.Models;

namespace E_Commerce.Api.DTOs;

public class ProductReadDto
{
    public string CategoryId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public IEnumerable<ImageFile> Images { get; set; } = new List<ImageFile>();

    public uint QuantityInStock { get; set; }
}