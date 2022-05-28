using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public class Category : BaseModel
{
    public Category()
    {
        Name = string.Empty;
        Image = new ImageFile("");
    }
    public Category(string name, ImageFile image)
    {
        Name = name;
        Image = image;
    }

    [BsonRequired] public string Name { get; set; }

    [BsonRequired] public ImageFile Image { get; set; }

    public void Update(string name, ImageFile image)
    {
        Name = name;
        Image = image;
        SetUpdated();
    }
}