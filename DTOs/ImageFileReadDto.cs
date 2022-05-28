namespace E_Commerce.Api.DTOs;

public class ImageFileReadDto
{
    public string Id { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}