using Microsoft.AspNetCore.Identity;

namespace GPUStoreMVC.Models.Data
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
