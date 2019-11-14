using MyApp.Common.Models;
using MyApp.Common.Services;
using Prism.Commands;
using Prism.Navigation;

namespace MyApp.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private string _password;
        private bool _isRunning;
        private bool _isEnabled;
        private DelegateCommand _loginCommand;

        public LoginPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Login";
            IsEnabled = true;
            //TODO Borras estas dos líneas:
            Email = "gringo@yopmail.com";
            Password = "123456";
        }

        public DelegateCommand LoginCommand => _loginCommand ?? (_loginCommand = new DelegateCommand(Login));

        public string Email { get; set; }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar un mail.", "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar un password.", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;


            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Chequee su conección a Internet.", "Aceptar");
                return;
            }




            var request = new TokenRequest
            {
                Password = Password,
                Username = Email
            };

            var response = await _apiService.GetTokenAsync(url, "Account", "/CreateToken", request);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Usuario o Password incorrecto.", "Aceptar");
                Password = string.Empty;
                return;
            }

            var token = response.Result;
            var response2 = await _apiService.GetTechnicalByEmailAsync(
                url,
                "api",
                "/Technicals/GetTechnicalByEmail",
                "bearer",
        token.Token,
                Email);
            if (!response2.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await App.Current.MainPage.DisplayAlert("Error", "Problema con datos de este usuario.", "Aceptar");
                Password = string.Empty;
                return;
            }

            var technical = response2.Result;
            var parameters = new NavigationParameters
            {
                {"technical",technical}
            };

            await _navigationService.NavigateAsync("VisitsPage", parameters);
            IsRunning = false;
            IsEnabled = true;

        }
    }
}
