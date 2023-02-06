using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;

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

    }
}
