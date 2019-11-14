using MyApp.Common.Models;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MyApp.Prism.ViewModels
{
    public class VisitsPageViewModel : ViewModelBase
    {
        private TechnicalResponse _technical;
        private ObservableCollection<VisitResponse2> _visits;
        public VisitsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Visitas";
        }

        public ObservableCollection<VisitResponse2> Visits
        {
            get => _visits;
            set => SetProperty(ref _visits, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.ContainsKey("technical"))
            {
                _technical = parameters.GetValue<TechnicalResponse>("technical");
                Title = $"Visitas de: {_technical.FullName}";
                Visits = new ObservableCollection<VisitResponse2>(_technical.Visits);
            }
        }
    }
}
