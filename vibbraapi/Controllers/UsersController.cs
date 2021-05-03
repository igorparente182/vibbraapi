using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using vibbraapi.Domain.Commands;
using vibbraapi.Domain.Entities;
using vibbraapi.Domain.Handler;
using vibbraapi.Domain.Repositories;

namespace vibbraapi.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UsersController : Controller
    {

        public readonly IUserRepository _repository;

        public UsersController(IUserRepository repository) 
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [Route("authenticate")]
        [HttpPost]
        public JsonResult Authenticate([FromBody] AuthCommand command,
            [FromServices] UserHandle handle,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations
            )
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };
            try
            {
                var auth = (GenericCommandResult)handle.Handle(command);

                if (!auth.Success)
                {
                    return new JsonResult(BadRequest())
                    {
                        StatusCode = 400,
                        Value = new GenericCommandResult(false, auth.Message, command.Notifications)
                    };
                }
                else
                {
                    return Json(new GenericCommandResult(true, auth.Message, new { }));
                }


            }
            catch (Exception e)
            {
                return new JsonResult(BadRequest())
                {
                    StatusCode = 400,
                    Value = e.Message
                };
            }
        }

        [HttpGet]
        public JsonResult GetById(long id)
        {
            if (id==0) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                
                return Json(_repository.GetById(id));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message) };
            }
        }

        [HttpPost]
        public JsonResult Create([FromBody] CreateUserCommand command,
            [FromServices] UserHandle handle
            )
        {
            if(command is null)  return new JsonResult(NotFound()) { StatusCode = 404 };

            try 
            {
                return Json((GenericCommandResult)handle.Handle(command));
            }catch(Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message) };
            }
        }

        [HttpPut]
        public JsonResult Update([FromBody] UpdateUserCommand command,
           [FromServices] UserHandle handle
           )
        {
            if (command is null) return new JsonResult(NotFound()) { StatusCode = 404 };

            try
            {
                var data = (GenericCommandResult)handle.Handle(command);
                if(data.Success)
                    return Json(data.Data);
                if (!data.Success)
                    return Json(data.Message);
                return Json("Error ao atualizar");
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest()) { StatusCode = 400, Value = new GenericCommandResult(false, "ops! Algo de errado ocorreu, ", ex.Message) };
            }
        }
        private object JWTToken(string name, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            Claim[] claims = new[]
              {
                new Claim(ClaimTypes.Name, name)

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Bearer");
            //if (userDTO.SistemaAbrev == Domain.Enums.SistemasAbrev.SISAVP)
            //    foreach (var perm in userDTO.Permissoes)
            //    {
            //        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, perm.Serv));
            //    }
            //if (userDTO.SistemaAbrev == Domain.Enums.SistemasAbrev.SISGU)
            //    foreach (var perm in userDTO.Permissoes)
            //    {
            //        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, perm.Serv ));
            //    }





            DateTime createDate = DateTime.Now;
            //DateTime endDate = createDate +
            //                         TimeSpan.FromSeconds(tokenConfigurations.Seconds);
            DateTime endDate = createDate +
                                    TimeSpan.FromHours(2);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = claimsIdentity,

                NotBefore = createDate,
                Expires = endDate
            });
            return handler.WriteToken(securityToken);

        }


    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }

}
