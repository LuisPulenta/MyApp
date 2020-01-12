using MyApp.Common.Helpers;
using MyApp.Common.Models;
using MyApp.Common.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
namespace MyApp.Prism.ViewModels
{
    public class VisitDetailItemViewModel : VisitDetailResponse
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private DelegateCommand _selectQuestionCommand;

        public DelegateCommand SelectQuestionCommand => _selectQuestionCommand ?? (_selectQuestionCommand = new DelegateCommand(SelectQuestion));

        public VisitDetailItemViewModel(INavigationService navigationService,
            IApiService apiService)
        {
            _navigationService = navigationService;
            this._apiService = apiService;
        }

        private async void SelectQuestion()
        {
            Settings.Question = JsonConvert.SerializeObject(this);

            await _navigationService.NavigateAsync("VisitDetailPage");


        }
    }
}