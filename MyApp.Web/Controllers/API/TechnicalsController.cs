using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Common.Models;
using MyApp.Web.Data;
using MyApp.Web.Data.Entities;
using MyApp.Web.Helpers;

namespace MyApp.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TechnicalsController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public TechnicalsController(
            DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        [HttpPost]
        [Route("GetTechnicalByEmail")]
        public async Task<IActionResult> GetTechnical(EmailRequest emailRequest)
        {
            try
            {
                var user = await _userHelper.GetUserByEmailAsync(emailRequest.Email);
                if (user == null)
                {
                    return BadRequest("Usuario no encontrado.");
                }

                if (await _userHelper.IsUserInRoleAsync(user, "Techcnical"))
                {
                    return await GetTechnicalAsync(emailRequest);
                }
                else
                {
                    return await GetTechnicalAsync(emailRequest);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

       

        private async Task<IActionResult> GetTechnicalAsync(EmailRequest emailRequest)
        {
            var technical = await _dataContext.Technicals
                .Include(o => o.User)
                .Include(o => o.Visits)
                .ThenInclude(o => o.Company)
                .Include(o => o.Visits)
                .ThenInclude(o => o.Technical)
                .Include(o => o.Visits)
                .ThenInclude(o => o.State)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower().Equals(emailRequest.Email.ToLower()));

            var response = new TechnicalResponse
            {
                Document = technical.User.Document,
                FirstName = technical.User.FirstName,
                LastName = technical.User.LastName,
                Address = technical.User.Address,
                FullName = technical.User.FullName,

                Visits = technical.Visits?.Select(p => new Common.Models.VisitResponse2
                {
                    CompanyId= p.Company.Id,
                    CompanyName = p.Company.Name,
                    Date = p.Date,
                    StateId = p.State.Id,
                    StateName = p.State.Name,
                    TechnicalId = p.Technical.Id,
                    TechnicalName = p.Technical.User.FullName,
                }).ToList()
            };
            return Ok(response);
        }


              


        private async Task<IActionResult> GetTechnicalAsync2(EmailRequest emailRequest)
        {
            var technical = await _dataContext.Technicals
                .Include(o => o.User)
                .Include(o => o.Visits)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower().Equals(emailRequest.Email.ToLower()));

            var response = new TechnicalResponse2
            {
                Document = technical.User.Document,
                FirstName = technical.User.FirstName,
                LastName = technical.User.LastName,
                Address = technical.User.Address,
                FullName = technical.User.FullName,

                Visits2= technical.Visits?.Select(p => new Common.Models.VisitResponse2
                {
                    Id=p.Id,
                    CompanyId=p.Company.Id,
                    CompanyName=p.Company.Name,
                    StateId=p.State.Id,
                    StateName=p.State.Name,
                    TechnicalId=p.Technical.Id,
                    TechnicalName=p.Technical.User.FullName,
                    Date = p.Date,
                      
                 
                }).ToList()
            };
            return Ok(response);
        }

    }
}
