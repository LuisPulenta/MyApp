using System;
using System.Linq;
using System.Threading.Tasks;
using MyApp.Web.Data.Entities;
using MyApp.Web.Helpers;

namespace MyApp.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public SeedDb(
                DataContext context,
                IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }


        
        

        #region Constructor
        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRoles();
            var manager = await CheckUserAsync("1010", "Luis", "Nuñez", "luisalbertonu@gmail.com", "156814963", "Espora 2052", "Manager");
            var technical = await CheckUserAsync("2020", "Luis", "Nuñez", "luis.albiazul@hotmail.com", "156814963", "Espora 2052", "Technical");
            var customer = await CheckUserAsync("3030", "Luis", "Nuñez", "luis.nunez@solflix.com", "156814963", "Espora 2052", "Customer");
            await CheckCompanyTypesAsync();
            await CheckCompanyAsync();
            await CheckStateAsync();

            //await CheckVisitAsync();
            //await CheckTechnicalAsync();
            //await CheckQuestionTypeAsync();
            //await CheckQuestionAsync();
        }
        #endregion

        #region Methods

        private async Task CheckRoles()
        {
            await _userHelper.CheckRoleAsync("Manager");
            await _userHelper.CheckRoleAsync("Technical");
            await _userHelper.CheckRoleAsync("Customer");
        }
        private async Task CheckManagerAsync(User user)
        {
            if (!_context.Managers.Any())
            {
                _context.Managers.Add(new Manager { User = user });
                await _context.SaveChangesAsync();
            }
        }


        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, string role)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }







        private async Task CheckCompanyTypesAsync()
        {
            if (!_context.CompanyTypes.Any())
            {
                _context.CompanyTypes.Add(new Entities.CompanyType { Name = "Depósito" });
                _context.CompanyTypes.Add(new Entities.CompanyType { Name = "Estación de Servicio" });
                _context.CompanyTypes.Add(new Entities.CompanyType { Name = "Industria Metalmecánica" });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckCompanyAsync()
        {
            var companytype = _context.CompanyTypes.FirstOrDefault();
            var customer = _context.Users.FirstOrDefault();
            if (!_context.Companies.Any())
            {
                AddCompany(customer, "YPF La Garlopa", "-34.4704034", "-58.71428245", companytype);
                
                await _context.SaveChangesAsync();
            }
        }

        private void AddCompany(User customer, string name, string grXX, string gRYY, CompanyType companyType)
        {
            _context.Companies.Add(new Company
            {
                User=customer,
                Name = name,
                GRXX =grXX,
                GRYY=gRYY,
                CompanyType=companyType,
            });
        }

        private async Task CheckStateAsync()
        {
            if (!_context.States.Any())
            {
                _context.States.Add(new Entities.State { Name = "Programado" });
                _context.States.Add(new Entities.State { Name = "Realizado" });
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}
