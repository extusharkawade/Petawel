using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;
using System.Data.SqlClient;

namespace Petawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly IConfiguration _configuration;
  
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpGet]
        [Route("AllCategories")]
        public Response AllCategories()
        {
            DbConnections dbConnections = new DbConnections(_configuration);

            Response response = new Response();
           response =dbConnections.getAllCategories();
            
            return response;
        }

        [HttpGet]
        [Route("Category")]
        public Response Categoryint(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.ProductbyCategory(id,sqlConnection);
            return response;
        }
    }
}
