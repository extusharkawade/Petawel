using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petawel.Controllers.Models;
using Petawel.DTO;
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
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        private  IDictionary<string, string> users = new Dictionary<string, string>();


        public ProductController(IConfiguration configuration,JwtAuthenticationManager jwtAuthenticationManager)
        {
            _configuration = configuration;
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }

      
        [HttpGet]
        [AllowAnonymous]
        [Route("Test")]
        public string Test()
        {
            return "working";
        }

        [HttpGet]
      //  [Authorize]
        [Route("Product")]
        public Response Products(int ProdId)
        { 
           // Response response = new Response();
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response =dbConnections.FindProductById(ProdId);
          
            return response;
        }

        //Below API Retrives All data without any Id
        [HttpGet]
    //    [Authorize]
        [Route("getAllitems")]
        public Response Products()
        {
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.getAllProduct();
            return response;
        }
        [HttpPost]
      //  [Authorize]
        [Route("Product_Update")]
        public Response ProductU(int ProdId, string name, int price, string details, int availablity, string path)
        {
            DbConnections dbConnections = new DbConnections(_configuration);
            ProductModel product= new ProductModel();
            product.ProdName = name;
            product.ProdPrice= price;
            product.ProdDetails = details;
            product.AvailableQuantity = availablity;
            product.ImagePath = path;
            Response response = dbConnections.UpdateProduct(ProdId, product );
            return response;
        }

        [HttpPost]
       // [Authorize]
        [Route("SaveProduct")]
        public Response SaveProduct(SaveProductDto product)
        {
            DbConnections dbConnections = new DbConnections(_configuration);

            Response response =   dbConnections.SaveProduct(product);
            return response;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Registration")]
        public Response Registration(Registration registration)
        {
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.Registration( registration);
            return response;
        }



    }
}
