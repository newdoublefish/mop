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
    public class ManuOrdersController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public ManuOrdersController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IResponseOutput> Create([FromBody] ManuOrderCreateRequestDto requestDto)
        {
            var ret = await _fsql.Insert(_mapper.Map<ManuOrder>(requestDto)).ExecuteInsertedAsync();
            return ResponseOutput.Ok(ret.FirstOrDefault());
        }


        [HttpGet]
        [AllowAnonymous]

        async public Task<IResponseOutput> List([FromQuery] int page, int size)
        {
            var list = await _fsql.Select<ManuOrder>()
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<ManuOrder>
            {
                Page = page,
                Total = total,
                Size = size,
                List = list,
            });
        }

    }
}
