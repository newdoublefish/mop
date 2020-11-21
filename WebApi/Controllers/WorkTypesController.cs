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
    public class WorkTypesController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public WorkTypesController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IResponseOutput> List([FromQuery] string name, int page = 1, int size = 30)
        {
            var list = await _fsql.Select<WorkType>()
                .WhereIf(name != null, d => d.Name.Contains(name))
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<WorkType> { Page = page, Size = size, Total = total, List = list });
        }

        /// <summary>
        /// create department
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IResponseOutput> Create([FromBody] WorkTypeCreateRequestDto requestDto)
        {
            var workType = _mapper.Map<WorkType>(requestDto);
            workType.CreatedAt = DateTime.Now;
            var ret = await _fsql.Insert(workType).ExecuteInsertedAsync();
            return ResponseOutput.Ok(_mapper.Map<WorkTypeResponseDto>(ret.FirstOrDefault()));
        }

    }
}
