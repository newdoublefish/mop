using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public UsersController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpPost, ProducesResponseType(201)]
        [AllowAnonymous]
        async public Task<ActionResult> Create([FromBody] UserCreateRequestDto model)
        {
            var repo = _fsql.GetRepository<User>();
            User user = _mapper.Map<User>(model);
            await repo.InsertAsync(user);
            return Ok(user);
        }

        [HttpGet]
        [Authorize]
        public Task<List<User>> List([FromQuery] string key, [FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            return _fsql.Select<User>().WhereIf(!string.IsNullOrEmpty(key), a => a.UserName.Contains(key)).Page(page, size).ToListAsync();
        }
    }
}
