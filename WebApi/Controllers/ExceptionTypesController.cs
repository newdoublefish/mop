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
    public class ExceptionTypesController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public ExceptionTypesController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IResponseOutput> Create([FromBody] ExceptionTypeCreateRequestDto requestDto)
        {
            var ret = await _fsql.Insert(_mapper.Map<ExceptionType>(requestDto)).ExecuteInsertedAsync();
            return ResponseOutput.Ok(ret.FirstOrDefault());
        }


        [HttpGet]
        [AllowAnonymous]

        async public Task<IResponseOutput> List([FromQuery]int page, int size)
        {
            var list = await _fsql.Select<ExceptionType>()
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<ExceptionType>
            {
                Page = page,
                Total = total,
                Size = size,
                List = list,
            });
        }

    }
}
