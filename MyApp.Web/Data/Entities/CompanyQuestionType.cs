using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Web.Data.Entities
{
    public class CompanyQuestionType
    {
        public int Id { get; set; }
        public Company Company { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
