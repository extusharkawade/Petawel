using PetawelAdmin.DTO;

namespace PetawelAdmin.Models
{
    public class ResponseAdmin
    {

        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }

        public ProductModel Product { get; set; }
        public List<ProductModel> Products { get; set; }

        public Category category { get; set; }

        public List<Category> categories { get; set; }
    }
}
