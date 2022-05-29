using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public class Product : BaseModel
{
    public Product(string categoryId, string name, string description, decimal price, IEnumerable<ImageFile> images,
        uint quantityInStock)
    {
        Name = name;
        Description = description;
        Price = price;
        Images = images;
        QuantityInStock = quantityInStock;
        CategoryId = categoryId;
    }

    [BsonRequired] public string CategoryId { get;  set; }

    [BsonRequired] public string Name { get;  set; }

    [BsonRequired] public string Description { get;  set; }

    [BsonRequired] public decimal Price { get;  set; }

    [BsonRequired] public IEnumerable<ImageFile> Images { get;  set; }

    [BsonRequired] public uint QuantityInStock { get;  set; }

    public void Update(string categoryId, string name, string description, decimal price, uint quantityInStock, IEnumerable<ImageFile> images)
    {
        Images = images;
        Name = name;
        Description = description;
        Price = price;
        QuantityInStock = quantityInStock;
        CategoryId = categoryId;
        SetUpdated();
    }
}