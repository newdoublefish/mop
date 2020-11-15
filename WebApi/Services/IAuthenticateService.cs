using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Services
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginRequestDto request, out string token);
    }
}
