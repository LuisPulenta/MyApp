using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Models
{
    public class CompanyQuestionTypeViewModel
    {
        public Company Company { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionTypeId { get; set; }
        public IEnumerable<SelectListItem> QuestionTypes { get; set; }
    }
}
