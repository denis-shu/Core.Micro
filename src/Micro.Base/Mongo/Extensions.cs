using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Micro.Base.Mongo
{
    public static class Extensions
    {
         public static void AddMongoDB(this IServiceCollection services, IConfiguration config)
        {
            
            services.Configure<MongoOpt>(config.GetSection("mongo"));
            
            services.AddSingleton<MongoClient>(co =>
             {
                var options = co.GetService<IOptions<MongoOpt>>();

                return new MongoClient(options.Value.ConnectionString);
            });

             services.AddSingleton<IMongoDatabase>(c =>
             {
                var options = c.GetService<IOptions<MongoOpt>>();
                var client = c.GetService<MongoClient>();

                return client.GetDatabase(options.Value.Database);
            });

            services.AddSingleton<IDBInit, DBInit>();
            services.AddSingleton<IDBSeed, DbSeed>();


          
        }
    }
}