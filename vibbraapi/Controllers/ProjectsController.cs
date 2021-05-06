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
    [Route("api/v1/projects")]
    public class ProjectsController : Controller
    {
        private readonly IProjectRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ITimeRepository _userTimeRepository;

        public ProjectsController(IProjectRepository repository, IUserRepository userRepository,ITimeRepository userTimeRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _userTimeRepository = userTimeRepository;
        }

        [HttpGet]
        [Authorize]
        public JsonResult getAll()
        {

            try
            {
                    //Acesso ao repositorio para pegar uma lista de projetos
                    var result = _repository.getAll();
                    //Criar uma lista vazia para o retornar os dados no formato correto
                    List<ProjectDTO> ProjectsDTO = new List<ProjectDTO>();
                    foreach (var item in result)
                    {
                        var projectDTO = new ProjectDTO(item.Id, item.Title, item.Description);
                        //Adicionar o Dados na lista 
                        ProjectsDTO.Add(projectDTO);
                    }
                    //Mostrar o retorno
                    if (result != null) return Json(ProjectsDTO);
                    //Retornar mensagem de vazio caso não encontre o projeto
                    return Json("Not Found Project");
               

            }
            catch (Exception ex)
            {
                //Retorna algum erro exceção 
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message + ex.InnerException) };
            }
        }


        [HttpGet("{project_id:long}")]
        [Authorize]
        public JsonResult getById(long project_id)
        {

            try {

                if (project_id != 0)
                {
                    var result = _repository.getById(project_id);
                    var projectDTO = new ProjectDTO(result.Id, result.Title, result.Description);
                    if (result != null) return Json(projectDTO);

                    return Json("Not Found");
                }
                else {
                    var result = _repository.getAll();
                    List<ProjectDTO> ProjectsDTO = new List<ProjectDTO>();
                    foreach(var item in result)
                    {
                        var projectDTO = new ProjectDTO(item.Id, item.Title, item.Description);
                        ProjectsDTO.Add(projectDTO);
                    }
                   
                    if (result != null) return Json(ProjectsDTO);

                    return Json("Not Found");
                }
               

            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message + ex.InnerException) };
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Create([FromBody] CreateProjectCommand command, [FromServices] ProjectHandler handler,[FromServices] TimeHandler timeHandler) 
        {
            //Verificar se passou algum comando
            if(command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                //Executa o processor de criação de projeto
                var resultProject = (GenericCommandResult)handler.Handle(command);
               //Verificar se ocorreu com sucesso
                if (resultProject.Success)
                {
                    //Retorna um objeto do Tipo Projeto
                    var project = (Project)resultProject.Data;
                    //inicia a fase de colocar o usuario na tabela de tempo no projeto
                    foreach (var item in command.User_Id) {
                        var user = _userRepository.GetById(item);
                        //criar o tempo do projeto por usuario
                        var resultTime = timeHandler.Handle(new CreateTimeCommand(project.Id, user.Id, null, null));                     
                    }
                    //Transformo o retorno para mostrar os dados de forma mais organizada
                    var projectDTO = new ProjectDTO(project.Id, project.Title, project.Description);
                    return Json(projectDTO);
                   
                }
                //Caso não consiga inserir retornar a messagem
                return Json("Not insert project");
            }
            catch (Exception ex) 
            {
                //Retorna algum erro exceção 
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message+ex.InnerException) };
            }
        }

        [HttpPut]
        [Authorize]
        public JsonResult Update([FromBody] UpdateProjectCommand command, [FromServices] ProjectHandler handler, [FromServices] TimeHandler timeHandler)
        {
            //Verificar se passou algum comando
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                //Executa o processor de atualização de projeto
                var resultProject = (GenericCommandResult)handler.Handle(command);
                //Verificar se ocorreu com sucesso
                if (resultProject.Success)
                {
                    var project = (Project)resultProject.Data;
                    foreach (var item in command.User_Id)
                    {
                        var user = _userRepository.GetById(item);
                        var time = _userTimeRepository.getTimeByProjectByUser(project.Id, item);
                        if (time != null)
                        {
                            var resultTime = timeHandler.Handle(new UpdateTimeCommand(time.Id, project.Id, user.Id, time.Started_at, time.Ended_at));
                        }                        
                    }
                    var projectDTO = new ProjectDTO(project.Id, project.Title, project.Description);
                    return Json(projectDTO);

                }
                return Json("Not update project");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message + ex.InnerException) };
            }
        }


    }
}
