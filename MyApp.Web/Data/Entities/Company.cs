﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Data.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public User User { get; set; }

        [Display(Name = "Nombre Empresa")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Coord X")]
        public string GRXX { get; set; }
        [Display(Name = "Coord Y")]
        public string GRYY { get; set; }
        public CompanyType CompanyType { get; set; }
        public ICollection<CQType> CQTypes { get; set; }
        public ICollection<Visit> Visits { get; set; }
        




    }
}
