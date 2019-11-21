using System.ComponentModel.DataAnnotations;

namespace MyApp.Common.Models
{
    public class IdRequest
    {
        [Required]
        
        public string Id { get; set; }
    }
}