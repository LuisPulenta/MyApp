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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                .Include(o => o.VisitDetails)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower().Equals(emailRequest.Email.ToLower()));

            var response = new TechnicalResponse
            {
                Document = technical.User.Document,
                FirstName = technical.User.FirstName,
                LastName = technical.User.LastName,
                Address = technical.User.Address,
                FullName = technical.User.FullName,
                
                Visits = technical.VisitDetails?.Select(p => new Common.Models.VisitResponse
                {
                    
                    CompanyId = p.CompanyId,
                    CompanyName = p.CompanyName,
                    TechnicalId = p.TechnicalId,
                    TechnicalName = p.TechnicalName,
                    Date = p.Date,
                    StateId = p.StateId,
                    StateName = p.StateName,
                    QuestionTypeId = p.QuestionTypeId,
                    QuestionTypeName = p.QuestionTypeName,
                    IdSubject = p.IdSubject,
                    Subject = p.Subject,
                    Note = p.Note,
                    ImageUrl1 = p.ImageUrl1,
                    ImageUrl2 = p.ImageUrl2,
                    ImageUrl3 = p.ImageUrl3,
                    ImageUrl4 = p.ImageUrl4,
                }).ToList()
            };
            return Ok(response);
        }
    }
}
