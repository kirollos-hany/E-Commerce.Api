namespace E_Commerce.Api.DTOs;

public class CategoryReadDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string CreatedAt { get; set; } = string.Empty;

    public string UpdatedAt { get; set; } = string.Empty;

    public ImageFileReadDto Image { get; set; } = new();
}