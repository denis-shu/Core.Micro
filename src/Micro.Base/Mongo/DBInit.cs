using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Micro.Base.Mongo
{
    public class DBInit : IDBInit
    {
        private bool _initializer;
        private readonly bool _seed;
        private readonly IMongoDatabase _db;
        private readonly IDBSeed _seeder;

        public DBInit(IMongoDatabase db, IOptions<MongoOpt> options, IDBSeed seed, IDBSeed seeder)
        {
             seeder=_seeder;
            _db = db;
            _seed = options.Value.Seed;

        }
        public async Task InitAsync()
        {
            if (_initializer)
                return;

            RegisterConventions();
            _initializer = true;
            if (!_seed)
            {
                return;
            }
            await _seeder.SeedAsync();
        }



        private void RegisterConventions()
        {
            ConventionRegistry.Register("MicroConventions", new MongoConvention(), m => true);
        }


        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}