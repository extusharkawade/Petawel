namespace PetawelAdmin.DTO
{
    public class SaveProductDto
    {
        public string ProdName { get; set; }
        public float ProdPrice { get; set; }
        public string ProdDetails { get; set; }
        public int AvailableQuantity { get; set; }
        public string ImagePath { get; set; }
        public int CatId { get; set; }
    }
}
