using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.API.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Micro.API.Repos
{
    public class ActivityRepo : IActivityRepo
    {
        private readonly IMongoDatabase _db;

        public ActivityRepo(IMongoDatabase db)
        {
            _db = db;
        }
        public async Task AddASync(Activity model)
        {
            await Collection.InsertOneAsync(model);
        }

        public async Task<IEnumerable<Activity>> BrowserAsync(Guid userId)
        {
            return await Collection.AsQueryable()
            .Where(u => u.Id == userId)
            .ToListAsync();
        }

        public async Task<Activity> GetAsync(Guid id)
        {
            return await Collection.AsQueryable()
            .FirstOrDefaultAsync(c => c.Id == id);
        }

      

        private IMongoCollection<Activity> Collection =>
        _db.GetCollection<Activity>("Activities");
    }
}