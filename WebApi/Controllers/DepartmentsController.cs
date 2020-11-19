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
    public class DepartmentsController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public DepartmentsController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        /// <summary>
        /// create department
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<DepartmentResponseDto> Create([FromBody] DepartmentCreateRequestDto requestDto)
        {
            var department = _mapper.Map<Department>(requestDto);
            department.CreatedAt = DateTime.Now;
            var ret = await _fsql.Insert(department).ExecuteInsertedAsync();
            return _mapper.Map<DepartmentResponseDto>(ret.FirstOrDefault());
        }


        /// <summary>
        /// list department
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<Department>> List([FromQuery] string name, int parentId, int page=1, int size=30) {
            return await _fsql.Select<Department>()
                .WhereIf(parentId != 0, d => d.ParentId == parentId)
                .WhereIf(name != null, d => d.Name.Contains(name))
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
        }


        /// <summary>
        /// 删除指定部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), ProducesResponseType(204)]
        [AllowAnonymous]
        async public Task<DepartmentResponseDto> Delete([FromRoute] int id)
        {
            var ret = await _fsql.Delete<Department>().Where(a => a.Id == id).ExecuteDeletedAsync();
            return _mapper.Map<DepartmentResponseDto>(ret.FirstOrDefault());
        }



    }
}
