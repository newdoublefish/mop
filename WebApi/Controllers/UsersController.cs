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
    public class UsersController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public UsersController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// create user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, ProducesResponseType(201)]
        [AllowAnonymous]
        async public Task<IResponseOutput> Create([FromBody] UserCreateRequestDto requestDto)
        {
            Department department = await _fsql.Select<Department>().Where(d => d.Id == requestDto.DepartmentId).FirstAsync();
            if (department == null)
            {
                throw new ArgumentException("无该部门");
            }
            Role role = await _fsql.Select<Role>().Where(r => r.Id == requestDto.RoleId).FirstAsync();
            if (role == null)
            {
                throw new ArgumentException("无该职位");
            }

            if (await _fsql.Select<User>().Where(u => u.UserName == requestDto.UserName).CountAsync() > 0)
            {
                throw new ArgumentException($"用户{requestDto.UserName}已经存在");
            }

            using (var uow = _fsql.CreateUnitOfWork()) //使用 UnitOfWork 事务
            {
                User user = _mapper.Map<User>(requestDto);
                user.CreatedAt = DateTime.Now;
                var ret = await _fsql.Insert(user).WithTransaction(uow.GetOrBeginTransaction()).ExecuteInsertedAsync();
                user = ret.FirstOrDefault();
                var userRole = await _fsql.Insert(new UserRole { UserId = user.Id, RoleId = role.Id, CreatedAt = DateTime.Now })
                    .WithTransaction(uow.GetOrBeginTransaction()).ExecuteInsertedAsync();
                var responseDto = _mapper.Map<UserResponseDto>(user);
                uow.Commit();
                responseDto.RoleId = role.Id;
                responseDto.RoleName = role.Name;
                responseDto.DepartmentName = department.Name;
                return ResponseOutput.Ok(responseDto);
            }
        }

        /// <summary>
        /// list all user
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IResponseOutput> List([FromQuery] string key, [FromQuery] int page = 1, [FromQuery] int size = 20)
        {
            var list = await _fsql.Select<User>().From<Department, UserRole, Role>((u, d, ur, r) => u
                        .LeftJoin(a => a.DepartmentId == d.Id)
                        .LeftJoin(a => a.Id == ur.UserId)
                        .LeftJoin(a => ur.RoleId == r.Id))
                    .WhereIf(!string.IsNullOrEmpty(key), (u, d, ur, r) => u.UserName.Contains(key))
                    .Page(page, size)
                    .Count(out var total)
                    .ToListAsync((u, d, ur, r) => new UserResponseDto
                    {
                        Id = u.Id,
                        DepartmentName = d.Name,
                        RoleName = r.Name,
                    });
            return ResponseOutput.Ok(new Pagenation<UserResponseDto> { Page = page, Size = size, Total=total, List = list});
        }

        /// <summary>
        /// 获取指定ID的用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IResponseOutput> GetById([FromRoute] int id)
        {
            var user = await _fsql.Select<User>().From<Department, UserRole, Role>((u, d, ur, r) => u
                        .LeftJoin(a => a.DepartmentId == d.Id)
                        .LeftJoin(a => a.Id == ur.UserId)
                        .LeftJoin(a => ur.RoleId == r.Id))
                    .WhereIf(id > 0, (u, d, ur, r) => u.Id == id)
                    .FirstAsync((u, d, ur, r) => new UserResponseDto
                    {
                        Id = u.Id,
                        DepartmentName = d.Name,
                        RoleName = r.Name,
                    });
            return ResponseOutput.Ok(user);
        }


        /// <summary>
        /// 删除指定用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(204)]
        [AllowAnonymous]
        async public Task<IResponseOutput> Delete([FromRoute] int id)
        {
            using (var uow = _fsql.CreateUnitOfWork()) //使用 UnitOfWork 事务
            {
                var ret = await _fsql.Delete<Role>().Where(a => a.Id == id).ExecuteDeletedAsync();
                var userRole = await _fsql.Delete<UserRole>().Where(a => a.UserId == ret.FirstOrDefault().Id).ExecuteDeletedAsync();
                uow.Commit();
                return ResponseOutput.Ok(_mapper.Map<RoleResponseDto>(ret.FirstOrDefault()));
            }
        }
    }
}
