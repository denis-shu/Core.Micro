using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Micro.Services.Activities.Domain.Models;
using Micro.Services.Activities.Domain.Repos;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Micro.Services.Activities.Repository
{
    public class ActivityRepo : IActivityRepo
    {
        private readonly IMongoDatabase _db;

        public ActivityRepo(IMongoDatabase db)
        {
            _db = db;
        }

        public async Task<Activity> GetByIdAsync(Guid id)
        {
            return await Collection.AsQueryable()
             .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Activity activity)
        {
            await Collection.InsertOneAsync(activity);
        }


        private IMongoCollection<Activity> Collection =>
        _db.GetCollection<Activity>("Categories");
    }
}