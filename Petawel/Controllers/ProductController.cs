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

        public ProductController(IConfiguration configuration,JwtAuthenticationManager jwtAuthenticationManager)
        {
            _configuration = configuration;
            this.jwtAuthenticationManager = jwtAuthenticationManager;

        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody] LoginDto loginDto)
        {
            var token = jwtAuthenticationManager.Authenticate(loginDto.email, loginDto.Password);
            if(token==null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(token);
            }

        }

      
        [HttpGet]
        [AllowAnonymous]
        [Route("Test")]
        public string Test()
        {
            return "working";
        }

        [HttpGet]
        [Authorize]
        [Route("Product")]
        public Response Products(int ProdId)
        { 
           // Response response = new Response();
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections();
            Response response =dbConnections.FindProductById(ProdId,sqlConnection);
          
            return response;
        }
        //Below code Retrives All data without any Id
        
        [HttpGet]
        [Authorize]
        [Route("getAllitems")]
        public String Get()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM products", sqlConnection);
            DataTable products = new DataTable();
            da.Fill(products);
            if(products.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(products);
            }
            else
            {
                return "No Data found";
            }
                 
                }
        [HttpPost]
        [Authorize]
        [Route("Product_Update")]
        public Response ProductU(int ProdId, string name, int price, string details, int availablity, string path)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections();
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
        [Authorize]
        [Route("SaveProduct")]
        public Response SaveProduct(SaveProductDto product)
        {
            DbConnections dbConnections =new DbConnections();
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());

         Response response=   dbConnections.SaveProduct(product, sqlConnection);
            return response;
        }
    }
}
