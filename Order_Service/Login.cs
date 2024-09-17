using System.ComponentModel.DataAnnotations;

namespace Order_Service.API
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
