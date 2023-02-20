using Microsoft.AspNetCore.Mvc;
using PetawelAdmin.Models;
using System.Data.SqlClient;
using PetawelAdmin.DTO;
using System.Diagnostics.CodeAnalysis;

namespace PetawelAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
       
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        //  [Authorize]
        [Route("Product")]
        public ResponseAdmin Products(int ProdId)
        {
            //ResponseAdminResponseAdmin = newResponseAdmin();
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);
           ResponseAdmin ResponseAdmin = DbConnectionAdmin.FindProductById(ProdId);

            return ResponseAdmin;
        }

        //Below API Retrives All data without any Id
        [HttpGet]
        //    [Authorize]
        [Route("getAllitems")]
        public ResponseAdmin Products()
        {
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);
           ResponseAdmin ResponseAdmin = DbConnectionAdmin.getAllProduct();
            return ResponseAdmin;
        }
        [HttpPut]
        //  [Authorize]
        [Route("Product_Update")]
        public ResponseAdmin ProductU(SaveProductDto productDto, [NotNull]int productId)
        {
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);
           ResponseAdmin ResponseAdmin = DbConnectionAdmin.UpdateProduct(productDto,productId);
            return ResponseAdmin;
        }

        [HttpPost]
        // [Authorize]
        [Route("SaveProduct")]
        public ResponseAdmin SaveProduct(SaveProductDto product)
        {
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);

           ResponseAdmin ResponseAdmin = DbConnectionAdmin.SaveProduct(product);
            return ResponseAdmin;
        }

    }
}
