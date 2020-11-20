using AutoMapper;
using Common.Output;
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
        public async Task<IResponseOutput> Create([FromBody] RoleCreateRequestDto requestDto)
        {
            var role = _mapper.Map<Role>(requestDto);
            role.CreatedAt = DateTime.Now;
            var ret = await _fsql.Insert(role).ExecuteInsertedAsync();
            return ResponseOutput.Ok(_mapper.Map<RoleResponseDto>(ret.FirstOrDefault()));
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
        public async Task<IResponseOutput> List([FromQuery] string name, int page = 1, int size = 30)
        {
            var list = await _fsql.Select<Role>()
                .WhereIf(name != null, d => d.Name.Contains(name))
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<Role> { Page = page, Size = size, Total = total, List = list });
        }


        /// <summary>
        /// 删除指定角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(204)]
        [AllowAnonymous]
        async public Task<IResponseOutput> Delete([FromRoute] int id)
        {
            using (var uow = _fsql.CreateUnitOfWork()) //使用 UnitOfWork 事务
            {
                //删除用户角色关系
                await _fsql.Delete<UserRole>().Where(ur => ur.RoleId == id).ExecuteDeletedAsync();
                //删除角色关系
                var ret = await _fsql.Delete<Role>().Where(a => a.Id == id).ExecuteDeletedAsync();
                uow.Commit();
                return ResponseOutput.Ok(_mapper.Map<RoleResponseDto>(ret.FirstOrDefault()));
            }
        }


    }
}
