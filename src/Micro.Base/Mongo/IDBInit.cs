using System.Threading.Tasks;

namespace Micro.Base.Mongo
{
    public interface IDBInit
    {
         Task InitAsync();
    }
}