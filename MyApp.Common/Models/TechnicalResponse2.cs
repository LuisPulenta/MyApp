using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Common.Models
{
    public class TechnicalResponse2
    {
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string FullName { get; set; }
        public ICollection<VisitResponse> Visits2{ get; set; }

    }
}
