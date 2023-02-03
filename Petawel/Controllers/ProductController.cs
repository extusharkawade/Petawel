using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petawel.Controllers.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;

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
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response =dbConnections.FindProductById(ProdId,sqlConnection);
          
            return response;
        }

        //Below API Retrives All data without any Id
        [HttpGet]
        [Route("getAllitems")]
        public Response Products()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.getAllProduct(sqlConnection);
            return response;
        }

        [HttpPost]
        [Route("Product_Update")]
        public Response ProductU(int ProdId, string name, int price, string details, int availablity, string path)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            ProductModel product= new ProductModel();
            product.ProdName = name;
            product.ProdPrice= price;
            product.ProdDetails = details;
            product.AvailableQuantity = availablity;
            product.ImagePath = path;
            Response response = dbConnections.UpdateProduct(ProdId, sqlConnection, product );
            return response;
        }

        [HttpPost]
        [Route("Registration")]
        public Response Registration(Registration registration)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.Registration( registration,sqlConnection);
            return response;
        }

        [HttpGet]
        [Route("Category")]
        public Response Categoryint (int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.ProductbyCategory(id,sqlConnection);
            return response;
        }
        

    }
}
