using MyApp.Common.Models;
using MyApp.Common.Services;
using Prism.Navigation;
namespace MyApp.Prism.ViewModels
{
    public class VisitDetailItemViewModel : VisitDetailResponse
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public VisitDetailItemViewModel(INavigationService navigationService,
            IApiService apiService)
        {
            _navigationService = navigationService;
            this._apiService = apiService;
        }
    }
}