using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data.Entities;
namespace MyApp.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        #region Constructor
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        #endregion

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Technical> Technicals { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}