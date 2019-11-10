using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        
        public int? IdSubject { get; set; }

        [Display(Name = "Item a Relevar")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Subject { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
