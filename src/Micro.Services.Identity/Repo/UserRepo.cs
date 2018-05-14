using System;
using System.Threading.Tasks;
using Micro.Services.Identity.Domain.Models;
using Micro.Services.Identity.Domain.Repos;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Micro.Services.Identity.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly IMongoDatabase db;
        public UserRepo(IMongoDatabase db)
        {
            this.db = db;

        }
        private IMongoCollection<User> Collection => db.GetCollection<User>("Users");

        public async Task AddAsync(User u)
        {
            await Collection.InsertOneAsync(u);
        }

        public async Task<User> GetASync(Guid id)
        {
            return await Collection.AsQueryable()
             .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task<User> GetASync(string email)
        {
             return await Collection.AsQueryable()
             .FirstOrDefaultAsync(u => u.Email == email.ToLowerInvariant() );
        }

        // {
        //     set { db.GetCollection<User>("Users");}
        // }
    }
}