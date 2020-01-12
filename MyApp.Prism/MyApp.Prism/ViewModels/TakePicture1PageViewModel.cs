using MyApp.Common.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;


namespace MyApp.Prism.ViewModels
{
    public class TakePicture1PageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        
        

        private bool _isRunning;
        private bool _isEnabled;
        private ImageSource _imageSource;
        private MediaFile _file;
        
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

        public ImageSource ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        private DelegateCommand _cancelCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _takePictureCommand;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        public DelegateCommand SaveCommand => _saveCommand ?? (_cancelCommand = new DelegateCommand(Save));
        public DelegateCommand TakePictureCommand => _takePictureCommand ?? (_cancelCommand = new DelegateCommand(TakePicture));

        public TakePicture1PageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Tomar Fotografía 1";
            IsEnabled = true;
            ImageSource = "nophoto";
        }

        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }



        private async void TakePicture()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "De dónde quiere tomar la imagen?",
                "Cancelar",
                null,
                "Galería",
                "Nueva Foto");

            if (source == "Cancelar")
            {
                _file = null;
                return;
            }

            if (source == "Nueva Foto")
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }
            if (_file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
                var visitDetailPageViewModel = VisitDetailPageViewModel.GetInstance();
                visitDetailPageViewModel.ImageUrl1 = ImageSource.FromStream(() =>
                {
                    var stream = _file.GetStream();
                    return stream;
                });
                visitDetailPageViewModel.File1 = _file;
            }
            IsRunning = false;
        }


        private async void Save()
        {
            if (_file == null)
            {
                var visitDetailPageViewModel = VisitDetailPageViewModel.GetInstance();
                visitDetailPageViewModel.ImageUrl1 = "noimageavailable.png";
            }
            await _navigationService.GoBackAsync();
        }
    }
}