using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
