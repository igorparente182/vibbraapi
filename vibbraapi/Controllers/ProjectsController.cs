using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Handler;
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

        [HttpPost]
        public JsonResult Create([FromBody] CreateProjectCommand command, [FromServices] ProjectHandler handler,[FromServices] TimeHandler timeHandler) 
        {
            if(command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
               
                var resultProject = (GenericCommandResult)handler.Handle(command);
               
                if (resultProject.Success)
                {
                    var project = (Project)resultProject.Data;
                    foreach (var item in command.User_Id) {
                        var resultTime = timeHandler.Handle(new CreateTimeCommand(project.Id, item, null,null));
                    }
                    return Json(project);
                   
                }
                return Json("Not insert project");
            }
            catch (Exception ex) 
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message) };
            }
            return Json("ok");
        }
    }
}
