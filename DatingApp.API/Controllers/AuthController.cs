using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        private readonly IConfiguration config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            this.config = config;
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Reqister(UserForRegisterDto userForRegisterDto)
        {
            // TODO: validate the request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await this.repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exist");


            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await this.repo.Register(userToCreate, userForRegisterDto.Password);

            // ToDo: fix created routes
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForRegisterDto)
        {
            var userFromRepo = await this.repo.Login(userForRegisterDto.Password, userForRegisterDto.Username.ToLower());

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config.GetSection("AppSettings:Token").Value));
            var signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tockenDescripter = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tockenDescripter);
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });

        }

    }
}