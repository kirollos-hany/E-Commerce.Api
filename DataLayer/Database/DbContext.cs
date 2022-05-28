using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.Models;
using MongoDB.Driver;

namespace E_Commerce.Api.DataLayer.Database;

public class DbContext : IDbContext
{
    private readonly IMongoClient _mongoClient;

    private readonly IDatabaseConfiguration _configuration;

    public DbContext(IMongoClient mongoClient, IDatabaseConfiguration configuration)
    {
        _mongoClient = mongoClient;
        _configuration = configuration;
    }


    public IMongoCollection<Customer> Customers =>
        _mongoClient.GetDatabase(_configuration.DatabaseName)
            .GetCollection<Customer>(_configuration.CustomersCollection);

    public IMongoCollection<Product> Products => _mongoClient.GetDatabase(_configuration.DatabaseName)
        .GetCollection<Product>(_configuration.ProductsCollection);

    public IMongoCollection<ImageFile> Images => _mongoClient.GetDatabase(_configuration.DatabaseName)
        .GetCollection<ImageFile>(_configuration.ImagesCollection);

    public IMongoCollection<Category> Categories => _mongoClient.GetDatabase(_configuration.DatabaseName)
        .GetCollection<Category>(_configuration.CategoriesCollection);

    public IMongoCollection<Cart> Carts => _mongoClient.GetDatabase(_configuration.DatabaseName)
        .GetCollection<Cart>(_configuration.CartsCollection);

    public IMongoCollection<Order> Orders => _mongoClient.GetDatabase(_configuration.DatabaseName)
        .GetCollection<Order>(_configuration.OrdersCollection);
}