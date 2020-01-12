using System.Collections.ObjectModel;

namespace MyApp.Common.Models
{
    public class GroupedAnimal : ObservableCollection<Animal>
    {
        public string InitialLetter { get; set; }
        public string Type { get; set; }
    }
}
