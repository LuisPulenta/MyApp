using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Common.Models;
using MyApp.Web.Data;
using MyApp.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

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
                .Include(o => o.Visits)
                .ThenInclude(o => o.VisitDetails)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower().Equals(emailRequest.Email.ToLower()));

            var response = new TechnicalResponse
            {
                Document = technical.User.Document,
                FirstName = technical.User.FirstName,
                LastName = technical.User.LastName,
                Address = technical.User.Address,
                FullName = technical.User.FullName,

                Visits = technical.Visits?.Select(p => new Common.Models.VisitResponse
                {
                    Id = p.Id,
                    Date = p.Date,
                    CompanyName=p.Company.Name,
                    GRXX = p.Company.GRXX,
                    GRYY = p.Company.GRYY,
                    State = p.State.Name,

                    VisitDetails = p.VisitDetails?.Select(pi => new VisitDetailResponse
                    {
                       Id=pi.Id,
                       IdSubject = pi.IdSubject,
                       ImageUrl1 = pi.ImageUrl1,
                       ImageUrl2 = pi.ImageUrl2,
                       ImageUrl3 = pi.ImageUrl3,
                       ImageUrl4 = pi.ImageUrl4,
                       Note=pi.Note,
                       QuestionTypeId=pi.QuestionTypeId,
                       QuestionTypeName=pi.QuestionTypeName,
                       Subject=pi.Subject
                     }).ToList()
                }).ToList()
            };
            
            return Ok(response);
        }
    }
}

