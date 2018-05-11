using System.Threading.Tasks;

namespace Micro.Base.Mongo
{
    public interface IDBSeed
    {
         Task SeedAsync();
    }
}