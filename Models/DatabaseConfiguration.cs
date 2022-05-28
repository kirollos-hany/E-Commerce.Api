using E_Commerce.Api.DataLayer;

namespace E_Commerce.Api.Models;

public class DatabaseConfiguration : IDatabaseConfiguration
{
    public string DatabaseName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string CustomersCollection { get; set; } = string.Empty;
    public string OrdersCollection { get; set; } = string.Empty;
    public string ProductsCollection { get; set; } = string.Empty;
    public string CartsCollection { get; set; } = string.Empty;
    public string CategoriesCollection { get; set; } = string.Empty;
    public string ImagesCollection { get; set; } = string.Empty;
}