using System.Threading.Tasks;

namespace Micro.Base.Events
{
     public  interface IEventHanler<in T> where T:IEvent
    {
         Task HandleAsync(T command);
    }
}