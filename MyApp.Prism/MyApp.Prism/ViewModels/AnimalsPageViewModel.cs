using MyApp.Common.Models;
using MyApp.Common.Services;
using Prism.Navigation;
using System.Collections.ObjectModel;

namespace MyApp.Prism.ViewModels
{
    public class AnimalsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        public ObservableCollection<GroupedAnimal> groupedAnimal { get; set; }

        public AnimalsPageViewModel(INavigationService navigationService, IApiService apiService) : base(navigationService)
        {
            groupedAnimal = new ObservableCollection<GroupedAnimal>();

            var dogs = new GroupedAnimal()
            {
                Type = "Dog",
                InitialLetter = "D"
            };

            var cats = new GroupedAnimal()
            {
                Type = "Cat",
                InitialLetter = "C"
            };


            cats.Add(new Animal() { Name = "Kevin", Race = "British Shorthair" });
            cats.Add(new Animal() { Name = "Mike", Race = "Ragdoll" });
            cats.Add(new Animal() { Name = "Junior", Race = "Abisinio" });

            dogs.Add(new Animal() { Name = "Lucky", Race = "Beagle" });
            dogs.Add(new Animal() { Name = "Blackie", Race = "Negra" });
            dogs.Add(new Animal() { Name = "Milú", Race = "Terrier" });
            dogs.Add(new Animal() { Name = "Bonita", Race = "Pequinés" });
            dogs.Add(new Animal() { Name = "Flaco", Race = "Marrón" });

            groupedAnimal.Add(dogs);
            groupedAnimal.Add(cats);
        }
    }
}
