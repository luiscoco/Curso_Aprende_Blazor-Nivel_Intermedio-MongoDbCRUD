using MongoDB.Driver;

namespace BlazorApp2.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
        }

        public IMongoCollection<Product> Products
        {
            get { return _database.GetCollection<Product>(nameof(Product)); }
        }
    }
}
