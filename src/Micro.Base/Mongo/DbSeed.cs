using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Micro.Base.Mongo
{
    public class DbSeed : IDBSeed
    {
        private readonly IMongoDatabase _db;

        public DbSeed(IMongoDatabase db)
        {
            _db = db;
        }


        public async Task SeedAsync()
        {
            var collctn = await _db.ListCollectionsAsync();
            var list = await collctn.ToListAsync();
            if (list.Any())
                return;

            await CustomSeedAsync();
        }
    }
}