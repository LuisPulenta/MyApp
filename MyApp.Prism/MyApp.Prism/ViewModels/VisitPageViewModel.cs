using MyApp.Common.Helpers;
using MyApp.Common.Models;
using MyApp.Common.Services;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyApp.Prism.ViewModels
{
    public class VisitPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private VisitResponse _visit;
        
        private ObservableCollection<VisitDetailResponse> _visitsCollection;

        public VisitResponse Visit
        {
            get => _visit;
            set => SetProperty(ref _visit, value);
        }
        public ObservableCollection<VisitDetailResponse> VisitDetails
        {
            get => _visitsCollection;
            set => SetProperty(ref _visitsCollection, value);
        }

        public VisitPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
        
            Title = "Detalle Visita";
            LoadVisit();
        }

        private void LoadVisit()
        {
            _visit = JsonConvert.DeserializeObject<VisitResponse>(Settings.Visit);
            Title = $"Visita a: {_visit.CompanyName}";
            VisitDetails = new ObservableCollection<VisitDetailResponse>(_visit.VisitDetails.Select(p => new VisitDetailItemViewModel(_navigationService, _apiService)
            {
                Id = p.Id,
                IdSubject=p.IdSubject,
                ImageUrl1=p.ImageUrl1,
                ImageUrl2 = p.ImageUrl2,
                ImageUrl3 = p.ImageUrl3,
                ImageUrl4 = p.ImageUrl4,
                Note=p.Note,
                QuestionTypeId=p.QuestionTypeId,
                QuestionTypeName=p.QuestionTypeName,
                Subject=p.Subject,
            }).ToList());
        }
    }
}