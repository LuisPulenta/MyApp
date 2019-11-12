using System;

namespace MyApp.Web.Data.Entities
{
    public class VisitDetail
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int TechnicalId { get; set; }
        public DateTime Date { get; set; }
        public int StateId { get; set; }
        public int QuestionTypeId { get; set; }
        public int? IdSubject { get; set; }
        public string Note { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }
        public string ImageUrl4 { get; set; }

        public Company Company { get; set; }
        public Technical Technical { get; set; }
        public State State { get; set; }
        public QuestionType QuestionType { get; set; }
        public Question Question { get; set; }
        
    }
}
