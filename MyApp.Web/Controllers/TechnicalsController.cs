using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Manager")]
    public class TechnicalsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;
        private readonly IMailHelper _mailHelper;

        public TechnicalsController(DataContext context,IUserHelper userHelper,IMailHelper mailHelper)
        {
            _dataContext = context;
            _userHelper = userHelper;
            _mailHelper = mailHelper;
        }

        // GET: Technicals
        public IActionResult Index()
        {
            return View(_dataContext.Technicals
                .Include(o => o.User)
                .Include(v => v.Visits).OrderBy(o => o.User.LastName));
        }


        // GET: Technicals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var technical = await _dataContext.Technicals
                .Include(o => o.User)
                .Include(v => v.Visits)
                 .FirstOrDefaultAsync(o => o.Id == id);
            if (technical == null)
            {
                return NotFound();
            }
            return View(technical);
        }


        // GET: Technicals/Create
        public IActionResult Create()
        {
            var view = new AddUserViewModel { RoleId = 2 };
            return View(view);
        }


        // POST: Technicals/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await CreateUserAsync(model);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este Email ya está en uso.");
                    return View(model);
                }

                var technical = new Technical
                {
                    Visits = new List<Visit>(),
                    User = user,
                };

                _dataContext.Technicals.Add(technical);
                await _dataContext.SaveChangesAsync();

                var myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                var tokenLink = Url.Action("ConfirmEmail", "Account", new
                {
                    userid = user.Id,
                    token = myToken
                }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Username, "MyApp - Email confirmation", $"<h1>MyApp - Email Confirmation</h1>" +
                    $"Para autorizar el Usuario, " +
                    $"por favor haga clic en este link:</br></br><a href = \"{tokenLink}\">Confirm Email</a>");




                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        private async Task<User> CreateUserAsync(AddUserViewModel model)
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


        // GET: Technicals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technical = await _dataContext.Technicals
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (technical == null)
            {
                return NotFound();
            }

            var view = new EditUserViewModel
            {
                Address = technical.User.Address,
                Document = technical.User.Document,
                FirstName = technical.User.FirstName,
                Id = technical.Id,
                LastName = technical.User.LastName,
                PhoneNumber = technical.User.PhoneNumber
            };

            return View(view);
        }

        // POST: Technicals/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var technical = await _dataContext.Technicals
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                technical.User.Document = model.Document;
                technical.User.FirstName = model.FirstName;
                technical.User.LastName = model.LastName;
                technical.User.Address = model.Address;
                technical.User.PhoneNumber = model.PhoneNumber;

                await _userHelper.UpdateUserAsync(technical.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Technicals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technical = await _dataContext.Technicals
                .FirstOrDefaultAsync(m => m.Id == id);
            if (technical == null)
            {
                return NotFound();
            }

            return View(technical);
        }

        // POST: Technicals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technical = await _dataContext.Technicals.FindAsync(id);
            _dataContext.Technicals.Remove(technical);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TechnicalExists(int id)
        {
            return _dataContext.Technicals.Any(e => e.Id == id);
        }
    }
}
