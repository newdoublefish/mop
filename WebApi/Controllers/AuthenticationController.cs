using Common.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authService;

        public AuthenticationController(IAuthenticateService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost, Route("requestToken")]
        public IResponseOutput RequestToken([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return ResponseOutput.NotOk("Invalid Request");
            }

            string token;
            if (_authService.IsAuthenticated(request, out token))
            {
                return ResponseOutput.Ok(token);
            }

            return ResponseOutput.NotOk("Invalid Request");

        }
    }
}
