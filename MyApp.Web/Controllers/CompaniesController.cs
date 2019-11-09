using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyApp.Web.Data;
using MyApp.Web.Data.Entities;
using MyApp.Web.Helpers;
using MyApp.Web.Models;

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
                .Include(ct=>ct.CompanyType)
                .Include(v => v.Visits).OrderBy(p => p.Name));
        }



        // GET: Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var company = await _dataContext.Companies
                .Include(o => o.User)
                .Include(v => v.Visits)
                .Include(ct=>ct.CompanyType)
                 .FirstOrDefaultAsync(o => o.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public async Task<IActionResult> Create()
        {
            
            var model = new AddCompanyViewModel
            {
                CompanyTypes = _combosHelper.GetComboCompanyTypes()
            };
            model.CompanyTypes = _combosHelper.GetComboCompanyTypes();
            return View(model);
        }
        // POST: Companies/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este Email ya está en uso.");
                    model.CompanyTypes = _combosHelper.GetComboCompanyTypes();
                    return View(model);
                }

                var companyType = await _dataContext.CompanyTypes.FirstOrDefaultAsync(o => o.Id == model.CompanyTypeId);

                var company = new Company
                {
                    Visits = new List<Visit>(),
                    GRXX = model.GRXX,
                    GRYY = model.GRYY,
                    Name = model.Name,
                    User = user,
                    CompanyType=companyType,                };

                _dataContext.Companies.Add(company);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            model.CompanyTypes = _combosHelper.GetComboCompanyTypes();
            return View(model);
        }
        private async Task<User> CreateUserAsync(AddCompanyViewModel model)
        {
            var user = new User
            {
                Address = model.Address,
                Document = model.Document,
                Email = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Username
            };

            var result = await _userHelper.AddUserAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }
            var newUser = await _userHelper.GetUserByEmailAsync(model.Username);
            await _userHelper.AddUserToRoleAsync(newUser, "Technical");
            return newUser;
        }
        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _dataContext.Companies
                .Include(o => o.User)
                .Include (ct => ct.CompanyType)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (company == null)
            {
                return NotFound();
            }

            var companyType = await _dataContext.CompanyTypes.FirstOrDefaultAsync(o => o.Id == company.CompanyType.Id);

            var view = new EditCompanyViewModel
            {
                GRXX = company.GRXX,
                GRYY = company.GRYY,
                Name = company.Name,
                Address = company.User.Address,
                Document = company.User.Document,
                FirstName = company.User.FirstName,
                Id = company.Id,
                LastName = company.User.LastName,
                PhoneNumber = company.User.PhoneNumber,
                CompanyTypes = _combosHelper.GetComboCompanyTypes(),
                CompanyTypeId =company.CompanyType.Id
                
                

                
            };

            return View(view);
        }


        // POST: Companies/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = await _dataContext.Companies
                    .Include(o => o.User)
                    .Include(ct => ct.CompanyType)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                var companyType = await _dataContext.CompanyTypes.FirstOrDefaultAsync(o => o.Id == model.CompanyTypeId);


                company.User.Document = model.Document;
                company.User.FirstName = model.FirstName;
                company.User.LastName = model.LastName;
                company.User.Address = model.Address;
                company.User.PhoneNumber = model.PhoneNumber;

                company.GRXX = model.GRXX;
                company.GRYY = model.GRYY;
                company.Name = model.Name;
                company.CompanyType = companyType;



                _dataContext.Companies.Update(company);
                await _dataContext.SaveChangesAsync();

                await _userHelper.UpdateUserAsync(company.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        
      
        











        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _dataContext.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _dataContext.Companies.FindAsync(id);
            _dataContext.Companies.Remove(company);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _dataContext.Companies.Any(e => e.Id == id);
        }
    }
}
