using System.Threading.Tasks;

namespace Micro.Base.Commands
{
    public  interface ICommandHandler<in T> where T:ICommand
    {
         Task HandleAsync(T command);
    }
}