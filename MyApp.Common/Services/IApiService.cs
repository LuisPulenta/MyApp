using System.Threading.Tasks;
using MyApp.Common.Models;
namespace MyApp.Common.Services
{
    public interface IApiService
    {
        Task<Response<TechnicalResponse>> GetTechnicalByEmailAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            string tokenType,
            string accessToken,
            string email);
        Task<Response<TokenResponse>> GetTokenAsync(
            string urlBase,
            string servicePrefix,
            string controller,
            TokenRequest request);
        Task<bool> CheckConnectionAsync(string url);
    }
}