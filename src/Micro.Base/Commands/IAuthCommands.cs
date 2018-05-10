using System;

namespace Micro.Base.Commands
{
    public interface IAuthCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}