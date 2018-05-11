using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repos;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Micro.Services.Activities.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly IMongoDatabase _db;

        public CategoryRepo(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<Category> GetAsync(string name)
        {
            return await Collection.AsQueryable()
             .FirstOrDefaultAsync(c => c.NAme == name.ToLowerInvariant());
        }

        public async Task AddAsync(Category category)
        {
            await Collection.InsertOneAsync(category);
        }

        public async Task<IEnumerable<Category>> BrowseAsync()
        {
            return await Collection.AsQueryable()
              .ToListAsync();
        }

        private IMongoCollection<Category> Collection =>
        _db.GetCollection<Category>("Categories");

    }
}