using MyApp.Common.Helpers;
using MyApp.Common.Models;
using MyApp.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Linq;
namespace MyApp.Prism.ViewModels
{
    public class VisitsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TechnicalResponse _technical;
        private ObservableCollection<VisitItemViewModel> _visits;

        public VisitsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Visitas";
            LoadTechnical();
        }
        public ObservableCollection<VisitItemViewModel> Visits
        {
            get => _visits;
            set => SetProperty(ref _visits, value);
        }
        private void LoadTechnical()
        {
            _technical = JsonConvert.DeserializeObject<TechnicalResponse>(Settings.Technical);
            Title = $"Visitas de: {_technical.FullName}";
            Visits = new ObservableCollection<VisitItemViewModel>(_technical.Visits.Select(p => new VisitItemViewModel(_navigationService, _apiService)
            {
                Id = p.Id,
                State=p.State,
                CompanyName = p.CompanyName,
                Date = p.Date,
                GRXX=p.GRXX,
                GRYY = p.GRYY,
                VisitDetails = p.VisitDetails?.Select(pi => new VisitDetailResponse
                {
                    Id = pi.Id,
                    IdSubject = pi.IdSubject,
                    ImageUrl1 = pi.ImageUrl1,
                    ImageUrl2 = pi.ImageUrl2,
                    ImageUrl3 = pi.ImageUrl3,
                    ImageUrl4 = pi.ImageUrl4,
                    Note = pi.Note,
                    QuestionTypeId = pi.QuestionTypeId,
                    QuestionTypeName = pi.QuestionTypeName,
                    Subject = pi.Subject
                }).ToList()

            }).ToList());
        }
    }
}