using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public JsonResult GetByProject(long project_id) 
        {
            var times = _timeRepository.getTimeByProject(project_id);

            if (times != null)
                return Json(times);
            return Json("not found");
        }
    }
}
