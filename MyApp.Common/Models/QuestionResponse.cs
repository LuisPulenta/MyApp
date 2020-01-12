using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int? IdSubject { get; set; }
        public string Subject { get; set; }
        public string QuestionType { get; set; }
    }
}
