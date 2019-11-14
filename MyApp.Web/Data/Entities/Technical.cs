using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MyApp.Web.Data.Entities
{
    public class Technical
    {
        public int Id { get; set; }
        public User User { get; set; }
        public ICollection<Visit> Visits { get; set; }
        public ICollection<VisitDetail> VisitDetails { get; set; }

    }
}