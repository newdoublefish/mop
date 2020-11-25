using AutoMapper;
using Common.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManuOrderFeedbacksController : ControllerBase
    {
        IFreeSql _fsql;
        private readonly IMapper _mapper;

        public ManuOrderFeedbacksController(IFreeSql fsql, IMapper mapper)
        {
            _fsql = fsql;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        async public Task<IResponseOutput> Create([FromBody] ManuOrderFeedbackCreateRequestDto requestDto)
        {
            //检查工序, 其他大类等
            //检查用户是否存在
            var user = await _fsql.Select<User>().Where(u => u.Id == requestDto.UserId).FirstAsync();
            if (user == null) {
                return ResponseOutput.NotOk("用户不存在");
            }

            //检查工作类型
            var workType = await _fsql.Select<WorkType>().Where(w => w.Id == requestDto.WorkTypeId).FirstAsync();
            if (workType == null)
            {
                return ResponseOutput.NotOk("工作类型不存在");
            }

            //工作类型
            if (requestDto.ProcedureId != 0) {
                var procedure = await _fsql.Select<Procedure>().Where(p => p.Id == requestDto.ProcedureId).FirstAsync();
                if (procedure == null)
                {
                    return ResponseOutput.NotOk("工序不存在");
                }
            }

            //检查线材类型
            if (requestDto.WireRodTypeId != 0) {
                var wireRodType = await _fsql.Select<WireRodType>().Where(w => w.Id == requestDto.WireRodTypeId).FirstAsync();
                if (wireRodType == null)
                {
                    return ResponseOutput.NotOk("线材类型不存在");
                }
            }
            

            var feedBack = _mapper.Map<ManuOrderFeedback>(requestDto);
            feedBack.StartAt = DateTime.Now;
            feedBack.Status = 1;
            var ret = await _fsql.Insert(feedBack).ExecuteInsertedAsync();
            return ResponseOutput.Ok(ret.FirstOrDefault());
        }


        [HttpGet]
        [AllowAnonymous]

        async public Task<IResponseOutput> List([FromQuery] int page, int size)
        {
            var list = await _fsql.Select<ManuOrderFeedback>()
                .Count(out var total)
                .Page(page, size)
                .ToListAsync();
            return ResponseOutput.Ok(new Pagenation<ManuOrderFeedback>
            {
                Page = page,
                Total = total,
                Size = size,
                List = list,
            });
        }
    }
}
