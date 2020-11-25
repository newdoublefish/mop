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
using WebApi.Dtos;
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


        [HttpPut]
        [Route("{id}/finish")]
        async public Task<IResponseOutput> Finish([FromRoute]int id, [FromBody]ManuOrderFeedbackFinishUpdateDto updateDto) {
            var feedback = await _fsql.Select<ManuOrderFeedback>().Where(m => m.Id == id).FirstAsync();
            if (feedback == null) {
                return ResponseOutput.NotOk("报工单不存在");
            }

            //检查异常类型
            if (updateDto.ExceptionTypeId != 0)
            {
                var exceptionType = await _fsql.Select<ExceptionType>().Where(e => e.Id == updateDto.ExceptionTypeId).FirstAsync();
                if (exceptionType == null)
                {
                    return ResponseOutput.NotOk("异常类型不存在");
                }
            }

            _mapper.Map(updateDto, feedback);

            feedback.Status = 2;

            var ret = await _fsql.Update<ManuOrderFeedback>().SetSource(feedback).ExecuteUpdatedAsync();

            return ResponseOutput.Ok(ret.FirstOrDefault());
        }


        [HttpGet]
        [AllowAnonymous]

        async public Task<IResponseOutput> List([FromQuery] int page, int size)
        {
            var list = await _fsql.Select<ManuOrderFeedback>()
                .Include(m=>m.User)
                .Include(m=>m.WorkType)
                .Include(m=>m.Procedure)
                .Include(m=>m.WireRodType)
                .Include(m=>m.ExceptionType)
                
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
