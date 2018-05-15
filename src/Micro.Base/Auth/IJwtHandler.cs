using System;

namespace Micro.Base.Auth
{
    public interface IJwtHandler
    {
         JsonWebToken Create(Guid userId);
    }
}