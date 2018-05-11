using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Base.Mongo;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repos;
using MongoDB.Driver;

namespace Micro.Services.Activities.Services
{
    public class CustomDBSeeder : DbSeed
    {
        private readonly ICategoryRepo cat;
        public CustomDBSeeder(IMongoDatabase db, ICategoryRepo category) : base(db)
        {
            cat = category;
        }

        protected override async Task CustomSeedAsync()
        {
            var categories = new List<string>{
                "first",
                "second",
                "Burn, Motherfucker, Burn"
            };
            await Task.WhenAll(categories.Select(s =>
            cat.AddAsync(new Category(s))));
        }
    }
}