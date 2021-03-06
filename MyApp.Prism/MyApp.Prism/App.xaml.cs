﻿using Prism;
using Prism.Ioc;
using MyApp.Prism.ViewModels;
using MyApp.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Common.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyApp.Prism
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTY2MzIyQDMxMzcyZTMzMmUzMFVnNW5KSnM2dTZmRDljWm1RYTduQXFwRmNKSzVPWk1lT1JGSFRySXZCUTA9");
            InitializeComponent();
            //await NavigationService.NavigateAsync("NavigationPage/AnimalsPage");
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<VisitsPage, VisitsPageViewModel>();
            containerRegistry.RegisterForNavigation<VisitPage, VisitPageViewModel>();

            containerRegistry.RegisterForNavigation<AnimalsPage, AnimalsPageViewModel>();
            containerRegistry.RegisterForNavigation<VisitDetailPage, VisitDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<TakePicture1Page, TakePicture1PageViewModel>();
            containerRegistry.RegisterForNavigation<TakePicture2Page, TakePicture2PageViewModel>();
            containerRegistry.RegisterForNavigation<TakePicture3Page, TakePicture3PageViewModel>();
            containerRegistry.RegisterForNavigation<TakePicture4Page, TakePicture4PageViewModel>();
        }
    }
}
