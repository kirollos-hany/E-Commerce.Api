using E_Commerce.Api.DataLayer.Models;
using MongoDB.Driver;

namespace E_Commerce.Api.DataLayer.Database;

public interface IDbContext
{
    public IMongoCollection<Customer> Customers { get;  }
    
    public IMongoCollection<Product> Products { get; }
    
    public IMongoCollection<ImageFile> Images { get; }
    
    public IMongoCollection<Category> Categories { get; }
    
    public IMongoCollection<Cart> Carts { get; }
    
    public IMongoCollection<Order> Orders { get; }
}