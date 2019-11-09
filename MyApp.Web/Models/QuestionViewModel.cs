using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Models
{
    public class QuestionViewModel
    {
        public int IdSubject { get; set; }

        [Display(Name = "Item a Relevar")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Subject { get; set; }
        public int QuestionTypeId { get; set; }
    }
}