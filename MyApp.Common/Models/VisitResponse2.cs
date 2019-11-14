using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class VisitResponse2
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int TechnicalId { get; set; }
        public string TechnicalName { get; set; }
        public DateTime Date { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
    }
}
