using Microsoft.AspNetCore.Mvc;
using PetawelAdmin.Models;
using System.Data.SqlClient;
using PetawelAdmin.DTO;

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
        [HttpPost]
        //  [Authorize]
        [Route("Product_Update")]
        public ResponseAdmin ProductU(int ProdId, string name, int price, string details, int availablity, string path)
        {
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);
            ProductModel product = new ProductModel();
            product.ProdName = name;
            product.ProdPrice = price;
            product.ProdDetails = details;
            product.AvailableQuantity = availablity;
            product.ImagePath = path;
           ResponseAdmin ResponseAdmin = DbConnectionAdmin.UpdateProduct(ProdId, product);
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
