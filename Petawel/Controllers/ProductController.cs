using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;
using System.Data.SqlClient;

namespace Petawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
      
        [HttpGet]
        [Route("Test")]
        public string Test()
        {
            return "working";
        }

        [HttpGet]
        [Route("Product")]
        public Response Products(int ProdId)
        { 
           // Response response = new Response();
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections();
            Response response =dbConnections.FindProductById(ProdId,sqlConnection);
          
            return response;
        }

    }
}
