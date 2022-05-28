namespace E_Commerce.Api.Models;

public interface IDatabaseConfiguration
{
    public string DatabaseName { get; set; }
    public string ConnectionString { get; set; }
    public string CustomersCollection { get; set; }
    public string OrdersCollection { get; set; }
    public string ProductsCollection { get; set; }
    public string CartsCollection { get; set; }
    public string CategoriesCollection { get; set; }
    public string ImagesCollection { get; set; }
}