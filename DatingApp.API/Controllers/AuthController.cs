using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository repo;
        public AuthController(IAuthRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Reqister(string username, string password)
        {
            // TODO: validate the request

            var lowerUserName = username.ToLower();
            if (await this.repo.UserExists(username))
                return BadRequest("Username already exist");


            var userToCreate = new User
            {
                Username = username
            };
            
            var  createdUser = await this.repo.Register(userToCreate,password);

            // ToDo: fix created routes
            return StatusCode(201);


        }

    }
}