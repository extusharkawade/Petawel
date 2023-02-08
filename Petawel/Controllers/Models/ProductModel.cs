namespace Petawel.Controllers.Models
{
    public class ProductModel
    {
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public float ProdPrice { get; set; }
        public string ProdDetails { get; set; }
        public int AvailableQuantity { get; set; }
        public string ImagePath { get; set; }

        public int category_id { get; set; }

        public String category_name { get; set; }   

        public ProductModel(int prodId, string prodName, float prodPrice, string prodDetails, int availableQuantity, string imagePath)
        {
            ProdId = prodId;
            ProdName = prodName;
            ProdPrice = prodPrice;
            ProdDetails = prodDetails;
            AvailableQuantity = availableQuantity;
            ImagePath = imagePath;
            category_id = category_id;
        }

        public ProductModel(ProductModel productModel)
        {
            ProdId = productModel.ProdId;
            ProdName = productModel.ProdName;
            ProdPrice = productModel.ProdPrice;
            ProdDetails = productModel.ProdDetails;
            AvailableQuantity = productModel.AvailableQuantity;
            ImagePath = productModel.ImagePath;
            category_id = productModel.category_id;
        }

        public ProductModel()
        {
        }
    }

}
