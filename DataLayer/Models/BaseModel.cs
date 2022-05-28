using MongoDB.Bson.Serialization.Attributes;

namespace E_Commerce.Api.DataLayer.Models;

public abstract class BaseModel 
{
    public BaseModel()
    {
        Id = Guid.NewGuid().ToString("N");
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
    }

    [BsonId] 
    public string Id { get;  set; }

    public DateTime CreatedAt { get;  set; } 
    
    public DateTime UpdatedAt { get;  set; }

    protected void SetUpdated()
    {
        UpdatedAt = DateTime.Now;
    }
}