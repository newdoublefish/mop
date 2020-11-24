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
    public class ProceduresController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public ProceduresController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IResponseOutput> Create([FromBody] ProcedureCreateRequestDto requestDto) {
            var workType = await _fsql.Select<WorkType>().Where(w => w.Id == requestDto.WorkTypeId).FirstAsync();
            if (workType == null) {
                return ResponseOutput.NotOk($"worktype with id(${requestDto.WorkTypeId}) does not exsit");
            }

            var ret = await _fsql.Insert(_mapper.Map<Procedure>(requestDto)).ExecuteInsertedAsync();
            return ResponseOutput.Ok(ret.FirstOrDefault());
        }


        [HttpGet]
        [AllowAnonymous]

        async public Task<IResponseOutput> List([FromQuery] int workTypeId, int page, int size) {
            var list = await _fsql.Select<Procedure>()
                .WhereIf(workTypeId != 0, p => p.WorkTypeId == workTypeId)
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<Procedure> { 
                Page = page,
                Total = total,
                Size = size,
                List = list,
            });
        }


    }
}
