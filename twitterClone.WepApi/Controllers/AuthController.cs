using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using twiterClone.DAL.Classes.UserClases;
using twitterClone.Entity.Interfaces;
using twitterClone.WepApi.Models.AuthModels;

namespace twitterClone.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private IConfiguration configuration;

        public AuthController(IUnitOfWork _uow, IConfiguration _configuration)
        {
            uow = _uow;
            configuration = _configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]RegisterViewModel user)
        {
            if( await uow.AuthRepository.UserEmailExisist(user.Email))
            {
                ModelState.AddModelError("UserEmail", "User email alread exists");
            }else if(await uow.AuthRepository.UserEmailExisist(user.UserName))
            {
                ModelState.AddModelError("UserEmail", "User name alread exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = new UserClass()
            {
                Email = user.Email,
                UserName = user.UserName,
                Surname = user.Surname,
                Name=user.Name,
                Password=user.Password,
                UserImage=uow.BaseRepositoriys.SaveFile(user.postFiles, "UserImage")
            };
            var createdUser = await uow.AuthRepository.Register(model);

            return StatusCode(201,createdUser);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var logResuld = new LoginedModel();
            var user = await uow.AuthRepository.Login(model.UserName, model.Password);
            if (user == null)
                return Unauthorized();

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(configuration.GetSection("AppSettings:Token").Value);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(ClaimTypes.Email, user.Email),
                    
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDecriptor);
            var tokenstring = tokenHandler.WriteToken(token);
            logResuld.ID = user.ID;
            logResuld.Name = user.Name;
            logResuld.Surname = user.Surname;
            logResuld.Email = user.Email;
            logResuld.Token = tokenstring;

            return Ok(logResuld);
        }
    }
}
