using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands;
using vibbraapi.Domain.DTO;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Handler;
using vibbraapi.Domain.Repositories;

namespace vibbraapi.Controllers
{
    [ApiController]
    [Route("api/v1/times")]
    public class TimesController : Controller
    {
        public readonly ITimeRepository _timeRepository;

        public TimesController(ITimeRepository timeRepository) 
        {
            _timeRepository = timeRepository;
        }

        [HttpGet("{project_id:long}")]
        [Authorize]
        public JsonResult GetTimeBProjecty(long project_id) 
        {
            try { 
                var times = _timeRepository.getTimeByProject(project_id);

                if (times != null)
                {
                    List<TimeDTO> timesDTO = new List<TimeDTO>();
                    foreach(var item in times) 
                    {
                        var timeDTO = new TimeDTO(item.Id, item.Project, item.User, item.Started_at, item.Ended_at);
                        timesDTO.Add(timeDTO);
                    }
                    return Json(times);
                }
                    
                return Json("not found");
            }
            catch(Exception ex)
            {
                return new JsonResult(BadRequest())
                {
                    StatusCode = 400,
                    Value = new GenericCommandResult(false, ex.Message, null)
                };
            }
           
        }

        [HttpPost]
        public JsonResult Create([FromBody]CreateTimeCommand command,
            [FromServices]TimeHandler handler)
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try {

                var time = (GenericCommandResult)handler.Handle(command);
                
                if(!time.Success)
                    return new JsonResult(BadRequest())
                    {
                        StatusCode = 400,
                        Value = new GenericCommandResult(false, time.Message, command.Notifications)
                    };
                var data = (Time)time.Data;

              
               
                return Json(new TimeDTO(data.Id,data.Project,data.User,data.Started_at,data.Ended_at));
            }
            catch (Exception ex) {
                return new JsonResult(BadRequest())
                {
                    StatusCode = 400,
                    Value = new GenericCommandResult(false, ex.Message, command.Notifications)
                };
            }
        }

        [HttpPut]
        public JsonResult Update([FromBody] UpdateTimeCommand command,
          [FromServices] TimeHandler handler)
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {

                var time = (GenericCommandResult)handler.Handle(command);

                if (!time.Success)
                    return new JsonResult(BadRequest())
                    {
                        StatusCode = 400,
                        Value = new GenericCommandResult(false, time.Message, command.Notifications)
                    };
                var data = (Time)time.Data;


                return Json(new TimeDTO(data.Id, data.Project, data.User, data.Started_at, data.Ended_at));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest())
                {
                    StatusCode = 400,
                    Value = new GenericCommandResult(false, ex.Message, command.Notifications)
                };
            }
        }

    }
}
