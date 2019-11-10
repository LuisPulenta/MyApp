using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Data.Entities
{
    public class QuestionType
    {
        public int Id { get; set; }

        [Display(Name = "Tipos de Relevamientos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        
        public CompanyType CompanyType { get; set; }
        public ICollection<CQType> CQTypes { get; set; }

    }
}
