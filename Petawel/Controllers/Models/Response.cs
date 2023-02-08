using Petawel.DTO;

namespace Petawel.Controllers.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public ProductModel Product { get; set; }
        public List<ProductModel> Products { get; set; }        

        public List<Registration> registration { get; set; } 

        public Category? category { get; set; }

        //public List<Category>? category { get; set; }
        public List<Category> categories { get; set; }
    }
}
