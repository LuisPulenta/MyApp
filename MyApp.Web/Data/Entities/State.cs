using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Data.Entities
{
    public class State
    {
        public int Id { get; set; }

        [Display(Name = "Estado")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        public ICollection<Visit> Visit { get; set; }
    }
}
