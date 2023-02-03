using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;
using Petawel.DTO;
using System.Data.Common;
using System.Data.SqlClient;

namespace Petawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private IDictionary<string, string> users = new Dictionary<string, string>();


        public LoginController(IConfiguration configuration, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _configuration = configuration;
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }


        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] LoginDto loginDto)
        {
            DbConnections dbConnections = new DbConnections(_configuration);
            users = dbConnections.Credentials();
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






        /*     [HttpPost]
             public Response Login([FromBody] DTO.LoginDto model)
             {
                 SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());


                 string email = model.email;
                 string password = model.Password;

                 DbConnections dbConnections = new DbConnections();



                 return dbConnections.checkCredentials(email, password, sqlConnection);
             }
        */
    }

}
