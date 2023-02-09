using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetawelAdmin.Models;
using System.Data.SqlClient;
using PetawelAdmin.DTO;

namespace PetawelAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IConfiguration _configuration;

        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;

        }


        [HttpGet]
        [Route("AllCategories")]
        public ResponseAdmin AllCategories()
        {
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);

            ResponseAdmin ResponseAdmin = new ResponseAdmin();
            ResponseAdmin = DbConnectionAdmin.getAllCategories();

            return ResponseAdmin;
        }

        [HttpGet]
        [Route("Category")]
        public ResponseAdmin Categoryint(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("conn").ToString());
            DbConnectionAdmin DbConnectionAdmin = new DbConnectionAdmin(_configuration);
            ResponseAdmin ResponseAdmin = DbConnectionAdmin.ProductbyCategory(id, sqlConnection);
            return ResponseAdmin;
        }

    }
}
