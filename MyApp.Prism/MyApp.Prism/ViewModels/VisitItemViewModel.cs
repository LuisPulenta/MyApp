    using MyApp.Common.Helpers;
using MyApp.Common.Models;
using MyApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
namespace MyApp.Prism.ViewModels
{
    public class VisitItemViewModel : VisitResponse
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _selectVisitCommand;
        public VisitItemViewModel(INavigationService navigationService,
            IApiService apiService)
        {
            _navigationService = navigationService;
            this._apiService = apiService;
        }
        public DelegateCommand SelectVisitCommand => _selectVisitCommand ?? (_selectVisitCommand = new DelegateCommand(SelectVisit));

        private async void SelectVisit()
        {
            Settings.Visit = JsonConvert.SerializeObject(this);
            
            await _navigationService.NavigateAsync("VisitPage");


        }
    }
}