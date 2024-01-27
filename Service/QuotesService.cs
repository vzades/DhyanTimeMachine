using DhyanTimeMachine.Data.Entity;
using DhyanTimeMachine.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DhyanTimeMachine.Service
{
    public class QuotesService : IQuotesService
    {
        private readonly IMongoCollection<Quotes> _quotesCollection;

        public QuotesService(IOptions<DatabseSettings> quotesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            quotesDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                quotesDatabaseSettings.Value.DatabaseName);

            _quotesCollection = mongoDatabase.GetCollection<Quotes>(
                quotesDatabaseSettings.Value.CollectionName);
        }

        public async Task CreateAsync(Quotes quote) =>
    await _quotesCollection.InsertOneAsync(quote);


        public async Task<List<Quotes>> GetAsync( DateTime dateTime)
        {
           return  await _quotesCollection.Find(x => x.Date.Date == dateTime.Date).ToListAsync();
        }

        public async Task<List<Quotes>> GetAsync()
        {
            return await _quotesCollection.Find(_ => true).ToListAsync();
        }
    }
}
