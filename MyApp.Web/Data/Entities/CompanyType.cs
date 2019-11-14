using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyApp.Web.Data.Entities
{
    public class CompanyType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Empresa")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}