using Microsoft.AspNetCore.Mvc;
using PetawelAdmin.Models;
using System.Data.SqlClient;

namespace PetawelAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
       
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [Route("Home")]
        public ResponseAdmin getProduct()
        {
            //Response response = new Response();
            DbConnectionAdmin dbConnections = new DbConnectionAdmin(_configuration);
            Console.WriteLine(" in controller sqlconnection");
            ResponseAdmin response = dbConnections.getProduct();

            return response;
        }
    }
}
