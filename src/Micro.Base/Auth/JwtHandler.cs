using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Micro.Base.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOpt _opt;
        private readonly SecurityKey _sercurityKey;
        private readonly SigningCredentials _signingCredntls;
        private readonly JwtHeader _jwtHdr;
        private readonly TokenValidationParameters _tokenValParameters;
        public JwtHandler(IOptions<JwtOpt> opt)
        {
            _opt = opt.Value;
            _sercurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.ConfidentialKey));
            _signingCredntls = new SigningCredentials(_sercurityKey, SecurityAlgorithms.HmacSha256);
            _jwtHdr = new JwtHeader(_signingCredntls);
            _tokenValParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuer = _opt.Publisher,
                IssuerSigningKey = _sercurityKey
            };

        }
        public JsonWebToken Create(Guid userId)
        {
            var time = DateTime.UtcNow;
            var expDate = time.AddMinutes(_opt.ValidMinutes);
            var begin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expDate.Ticks - begin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(time.Ticks - begin.Ticks).TotalSeconds);
            var jwtPAyload = new JwtPayload{
                {"sub", userId},
                {"iss", _opt.Publisher},
                {"iat", now},
                {"exp", exp},
                {"unique_name", userId}
            };

            var jwr = new JwtSecurityToken(_jwtHdr, jwtPAyload);
            var token = _jwtSecTokenHandler.WriteToken(jwr);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}