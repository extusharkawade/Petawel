

using System.ComponentModel.DataAnnotations;

namespace PetawelAdmin.DTO
{
    public class SaveProductDto
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(100)]
        public string ProdName { get; set; }
     
        [Range(1, float.PositiveInfinity)]
        [Required]
        public float ProdPrice { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(255)]
        public string ProdDetails { get; set; }


        [Required]
        [Range(0,10000)]
        public int AvailableQuantity { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImagePath { get; set; }

        [DataType(DataType.Text)]
        [Range(1,double.PositiveInfinity)]
        [Required]
        public int CatId { get; set; }
    }
}
