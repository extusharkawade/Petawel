using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Petawel.Controllers.Models;
using Petawel.DTO;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
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
      //  [Authorize]
        [Route("Product")]
        public Response Product(int ProdId)
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


        [HttpGet]
        [Route("ProductsByCategory")]
        public Response ProductsByCategory(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnections dbConnections = new DbConnections(_configuration);
            Response response = dbConnections.ProductbyCategory(id, sqlConnection);
            return response;
        }






    }
}
