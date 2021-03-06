﻿    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Web.Data.Entities
{
    public class Visit
    {
        public int Id { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Company Company { get; set; }
        public Technical Technical { get; set; }
        public State State { get; set; }
        public ICollection<VisitDetail> VisitDetails { get; set; }
    }
}