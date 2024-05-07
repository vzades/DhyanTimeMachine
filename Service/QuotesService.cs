using DhyanTimeMachine.Data.Entity;
using DhyanTimeMachine.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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

        public Quotes GetRandomQuote()
        {
            var result =   _quotesCollection.AsQueryable().Sample(20);

            return result.First();
        }

        public async Task UpdateComments( string  id , Comment comment)
        {
           
            var filter = Builders<Quotes>.Filter.Eq("_id", ObjectId.Parse(id));
            var update = Builders<Quotes>.Update.Push("Comments", comment);

             await _quotesCollection.UpdateOneAsync(filter, update);

            
        }
    }
}
