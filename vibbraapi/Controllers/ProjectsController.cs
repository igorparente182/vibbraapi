using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vibbraapi.Domain.Repositories;

namespace vibbraapi.Controllers
{
    [ApiController]
    [Route("api/v1/projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _repository;

        public ProjectsController(IProjectRepository repository) 
        {
            _repository = repository;
        }
        [HttpGet]
        public JsonResult getAll()
        {
            var result = _repository.getAll();
            if (result != null)
                return Json(result);
            return Json("not found");
        }


    }
}
