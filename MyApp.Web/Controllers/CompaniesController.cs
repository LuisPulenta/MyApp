using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data;
using MyApp.Web.Helpers;
using System.Linq;
namespace MyApp.Web.Controllers
{
    public class CompaniesController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ICombosHelper _combosHelper;
        private readonly IUserHelper _userHelper;
        public CompaniesController(DataContext context, ICombosHelper combosHelper, IUserHelper userHelper)
        {
            _dataContext = context;
            _combosHelper = combosHelper;
            _userHelper = userHelper;
        }
        // GET: Companies
        public IActionResult Index()
        {
            return View(_dataContext.Companies
                .Include(o => o.User)
                .Include(ct => ct.CompanyType)
                //.Include(v => v.Visits)
                .Include(cq => cq.CQTypes)
                .OrderBy(p => p.Name));
        }
    }
}