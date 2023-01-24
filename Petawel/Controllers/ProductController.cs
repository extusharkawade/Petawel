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
            DbConnections dbConnections = new DbConnections();
            Response response =dbConnections.FindProductById(ProdId,sqlConnection);
          
            return response;
        }
        //Below code Retrives All data without any Id
        SqlConnection con = new SqlConnection("server=LAPTOP-HGF1J904\\SQLEXPRESS; database=Petawel; Integrated Security=true");
        [HttpGet]
        [Route("getAllitems")]
        public String Get()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM products", con);
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
    }
}
