using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly TokenManagement _tokenManagement;
        IFreeSql _fsql;

        public AuthenticateService(IOptions<TokenManagement> tokenManagement, IFreeSql fsql)
        {
            _tokenManagement = tokenManagement.Value;
            _fsql = fsql;
        }

        public bool IsAuthenticated(LoginRequestDto request, out string token)
        {
            token = string.Empty;
            /*            if (!_userService.IsValid(request))
                            return false;*/
            User user = _fsql.Select<User>().Where(u => u.UserName.Equals(request.Username) && u.Password.Equals(request.Password)).First();
            if (user == null) {
                return false;
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,request.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(_tokenManagement.Issuer, _tokenManagement.Audience, claims, expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration), signingCredentials: credentials);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return true;
        }
    }
}
