using E_Commerce.Api.DataLayer.Models;
using E_Commerce.Api.Models;
using MongoDB.Driver;

namespace E_Commerce.Api.HostedServices;

public class ConfigureMongoDb : IHostedService
{
    private readonly IMongoClient _client;

    private readonly ILogger<ConfigureMongoDb> _logger;

    private readonly IDatabaseConfiguration _databaseConfiguration;

    public ConfigureMongoDb(IMongoClient client, ILogger<ConfigureMongoDb> logger,
        IDatabaseConfiguration databaseConfiguration)
    {
        _client = client;
        _logger = logger;
        _databaseConfiguration = databaseConfiguration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var database = _client.GetDatabase(_databaseConfiguration.DatabaseName);
        var customersCollection = database.GetCollection<Customer>(_databaseConfiguration.CustomersCollection);

        _logger.LogInformation("Creating customer email unique index");
        var emailIndex = Builders<Customer>.IndexKeys.Ascending(c => c.Email);
        await customersCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<Customer>(emailIndex, new CreateIndexOptions() { Unique = true }),
            cancellationToken: cancellationToken);

        _logger.LogInformation("Creating category name unique index");
        var categoriesCollection = database.GetCollection<Category>(_databaseConfiguration.CategoriesCollection);
        var categoryNameIndex = Builders<Category>.IndexKeys.Ascending(c => c.Name);
        await categoriesCollection.Indexes.CreateOneAsync(
            new CreateIndexModel<Category>(categoryNameIndex, new CreateIndexOptions() { Unique = true }),
            cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}