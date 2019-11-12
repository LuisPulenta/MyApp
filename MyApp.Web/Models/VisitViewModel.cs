using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Models
{
    public class VisitViewModel
    {
        public int QuestionTypeId { get; set; }
        public int CompanyId { get; set; }
        public int StateId { get; set; }
        [Display(Name = "Técnico")]
        public int TechnicalId { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }


        public IEnumerable<SelectListItem> Technicals { get; set; }
    }
}
