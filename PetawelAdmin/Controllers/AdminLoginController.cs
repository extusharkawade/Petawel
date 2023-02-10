using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetawelAdmin.DTO;
using PetawelAdmin.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace PetawelAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private IDictionary<string, string> users = new Dictionary<string, string>();


        public AuthorizationController(IConfiguration configuration, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _configuration = configuration;
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] LoginDto loginDto)
        {
            // DbConnections dbConnections = new DbConnections(_configuration);
            // users = dbConnections.Credentials();
            DbConnectionAdmin dbConnectionAdmin = new DbConnectionAdmin(_configuration);
            if (dbConnectionAdmin.findAdmin(loginDto)==true)
            {
                Console.WriteLine("email" + loginDto.email);
                Console.WriteLine("Password" + loginDto.Password);
                users.Add(loginDto.email, loginDto.Password);

                var token = jwtAuthenticationManager.Authenticate(loginDto.email, loginDto.Password, users);
                if (token == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(token);
                }
            }
            else

            {
                return Unauthorized();
            }
            
           
           // return Ok(); 
        }
    }
}
