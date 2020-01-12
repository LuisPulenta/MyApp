using MyApp.Common.Helpers;
using MyApp.Common.Models;
using MyApp.Common.Services;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace MyApp.Prism.ViewModels
{
    public class VisitDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private VisitDetailResponse _visitDetail;
        private DelegateCommand _cancelCommand;
        private DelegateCommand _saveCommand;
        private DelegateCommand _takePicture1;
        private DelegateCommand _takePicture2;
        private DelegateCommand _takePicture3;
        private DelegateCommand _takePicture4;
        private ImageSource _imageUrl1;
        private ImageSource _imageUrl2;
        private ImageSource _imageUrl3;
        private ImageSource _imageUrl4;
        private MediaFile _file1;
        private MediaFile _file2;
        private MediaFile _file3;
        private MediaFile _file4;
        private bool _isRunning;
        private bool _isEnabled;

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));
        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));
        public DelegateCommand TakePicture1Command => _takePicture1 ?? (_takePicture1 = new DelegateCommand(TakePicture1));
        public DelegateCommand TakePicture2Command => _takePicture2 ?? (_takePicture2= new DelegateCommand(TakePicture2));
        public DelegateCommand TakePicture3Command => _takePicture3 ?? (_takePicture3 = new DelegateCommand(TakePicture3));
        public DelegateCommand TakePicture4Command => _takePicture4 ?? (_takePicture4 = new DelegateCommand(TakePicture4));


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
        public ImageSource ImageUrl1
        {
            get => _imageUrl1;
            set => SetProperty(ref _imageUrl1, value);
        }
        public ImageSource ImageUrl2
        {
            get => _imageUrl2;
            set => SetProperty(ref _imageUrl2, value);
        }
        public ImageSource ImageUrl3
        {
            get => _imageUrl3;
            set => SetProperty(ref _imageUrl3, value);
        }
        public ImageSource ImageUrl4
        {
            get => _imageUrl4;
            set => SetProperty(ref _imageUrl4, value);
        }
       
        public MediaFile File1
        {
            get => _file1;
            set => SetProperty(ref _file1, value);
        }
        public MediaFile File2
        {
            get => _file2;
            set => SetProperty(ref _file2, value);
        }
        public MediaFile File3
        {
            get => _file3;
            set => SetProperty(ref _file3, value);
        }

        public MediaFile File4
        {
            get => _file4;
            set => SetProperty(ref _file4, value);
        }

        public VisitDetailResponse VisitDetail
        {
            get => _visitDetail;
            set => SetProperty(ref _visitDetail, value);
        }

        public byte[] Image1 { get; set; }
        public byte[] Image2 { get; set; }
        public byte[] Image3 { get; set; }
        public byte[] Image4 { get; set; }

        public VisitDetailPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Pregunta";
            IsEnabled = true;
            VisitDetail = JsonConvert.DeserializeObject<VisitDetailResponse>(Settings.Question);
            instance = this;
            ImageUrl1 = VisitDetail.ImageFullPath1;
            ImageUrl2 = VisitDetail.ImageFullPath2;
            ImageUrl3 = VisitDetail.ImageFullPath3;
            ImageUrl4 = VisitDetail.ImageFullPath4;
        }

        #region Singleton

        private static VisitDetailPageViewModel instance;
        public static VisitDetailPageViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        private async void TakePicture1()
        {
            await _navigationService.NavigateAsync("TakePicture1Page");
        }
        private async void TakePicture2()
        {
            await _navigationService.NavigateAsync("TakePicture2Page");
        }
        private async void TakePicture3()
        {
            await _navigationService.NavigateAsync("TakePicture3Page");
        }
        private async void TakePicture4()
        {
            await _navigationService.NavigateAsync("TakePicture4Page");
        }
        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }
        private async void Save()
        {
            //Verificar que haya Nota
            if (string.IsNullOrEmpty(VisitDetail.Note))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Debe ingresar una Nota.", "Accept");
                return;
            }

            //Verificar conectividad
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsEnabled = true;
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Chequee su conexión a Internet.", "Aceptar");
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            byte[] im1array = null;
            if (File1 != null)
            {
                im1array = FilesHelper.ReadFully(this.File1.GetStream());
                File1.Dispose();
            }
            byte[] im2array = null;
            if (File2 != null)
            {
                im1array = FilesHelper.ReadFully(this.File2.GetStream());
                File2.Dispose();
            }
            byte[] im3array = null;
            if (File3 != null)
            {
                im3array = FilesHelper.ReadFully(this.File3.GetStream());
                File3.Dispose();
            }
            byte[] im4array = null;
            if (File4 != null)
            {
                im4array = FilesHelper.ReadFully(this.File4.GetStream());
                File4.Dispose();
            }

            //*********************************************************************************************************
            //Grabar 
            //*********************************************************************************************************

            var myVisitDetail = new VisitDetailResponse
            {
                IdSubject=VisitDetail.IdSubject,
                ImageUrl1= VisitDetail.ImageFullPath1,
                ImageUrl2 = VisitDetail.ImageFullPath2,
                ImageUrl3 = VisitDetail.ImageFullPath3,
                ImageUrl4 = VisitDetail.ImageFullPath4,
                Note= VisitDetail.Note,
                Subject= VisitDetail.Subject,
                //Revisar todos
            };

            var response = await _apiService.PutAsync(
                    url,
                    "api",
                    "/AsignacionesOTs",
                    myVisitDetail,
                    myVisitDetail.Id);

            IsRunning = false;
            IsEnabled = true;

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un error al guardar la Orden, intente más tarde.", "Aceptar");
                return;
            }

            await App.Current.MainPage.DisplayAlert("Ok", "Guardado con éxito!!", "Aceptar");
            //Recargar listado de preguntas
            await _navigationService.GoBackAsync();

        }
    }
}
