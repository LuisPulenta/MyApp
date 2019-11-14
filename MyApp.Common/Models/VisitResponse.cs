using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class VisitResponse
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int TechnicalId { get; set; }
        public string TechnicalName { get; set; }
        public DateTime Date { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int QuestionTypeId { get; set; }
        public string QuestionTypeName { get; set; }
        public int? IdSubject { get; set; }
        public string Subject { get; set; }
        public string Note { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }
    }
}
