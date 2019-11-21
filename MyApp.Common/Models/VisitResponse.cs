using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class VisitResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CompanyName { get; set; }
        public string GRXX { get; set; }
        public string GRYY { get; set; }
        public string State { get; set; }
        public ICollection<VisitDetailResponse> VisitDetails { get; set; }
    }
}
