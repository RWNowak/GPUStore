using System.ComponentModel.DataAnnotations;

namespace GPUStoreMVC.Models.Other
{
    public class Login
    {
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
