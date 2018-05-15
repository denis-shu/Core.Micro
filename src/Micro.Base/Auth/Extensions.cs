using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Micro.Base.Auth
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var opt = new JwtOpt();
            var section = config.GetSection("jwt");
            section.Bind(opt);
            services.Configure<JwtOpt>(section);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication().AddJwtBearer(c =>
            {
                c.RequireHttpsMetadata = false;
                c.SaveToken = true;
                c.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidIssuer = opt.Publisher,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(opt.ConfidentialKey))
                };
            });
        }
    }
}