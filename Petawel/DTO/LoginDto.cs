using System.ComponentModel.DataAnnotations;

namespace Petawel.DTO
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
