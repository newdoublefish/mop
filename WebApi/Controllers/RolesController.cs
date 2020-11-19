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
    public class RolesController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public RolesController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// create role
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<RoleResponseDto> Create([FromBody] RoleCreateRequestDto requestDto)
        {
            var role = _mapper.Map<Role>(requestDto);
            role.CreatedAt = DateTime.Now;
            var ret = await _fsql.Insert(role).ExecuteInsertedAsync();
            return _mapper.Map<RoleResponseDto>(ret.FirstOrDefault());
        }

        /// <summary>
        /// list roles
        /// </summary>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Role>> List([FromQuery] string name, int page = 1, int size = 30)
        {
            return await _fsql.Select<Role>()
                .WhereIf(name != null, d => d.Name.Contains(name))
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
        }


        /// <summary>
        /// 删除指定角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(204)]
        [AllowAnonymous]
        async public Task<RoleResponseDto> Delete([FromRoute] int id)
        {
            var ret = await _fsql.Delete<Role>().Where(a => a.Id == id).ExecuteDeletedAsync();
            return _mapper.Map<RoleResponseDto>(ret.FirstOrDefault());
        }


    }
}
