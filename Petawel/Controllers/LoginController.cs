using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace Petawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        public Response Login([FromBody] DTO.LoginDto model)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());


            string email = model.email;
            string password = model.Password;

            DbConnections dbConnections = new DbConnections();



            return dbConnections.checkCredentials(email, password, sqlConnection);
        }
    }
}
