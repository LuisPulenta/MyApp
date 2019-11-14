using System.ComponentModel.DataAnnotations;

namespace MyApp.Common.Models
{
    public class EmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}