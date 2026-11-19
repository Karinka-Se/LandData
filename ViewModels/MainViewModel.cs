using System.Collections.ObjectModel;
using LandData.Models;
using LandData.Services;

namespace LandData.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<Region> _allRegions;
        private string _searchText = "";
        private Region? _selectedRegion;

        // Список, который показывает окно (после фильтра по поиску)
        public ObservableCollection<Region> Regions { get; } = new();

        public MainViewModel()
        {
            _allRegions = new RegionService().GetAll();
            ApplyFilter();
        }

        public string SearchText
        {
            get => _searchText;
            set { if (SetProperty(ref _searchText, value)) ApplyFilter(); }
        }

        public Region? SelectedRegion
        {
            get => _selectedRegion;
            set => SetProperty(ref _selectedRegion, value);
        }

        private void ApplyFilter()
        {
            string text = SearchText.Trim();
            var found = _allRegions.Where(r =>
                text == "" ||
                r.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase) ||
                r.Code.StartsWith(text));

            Regions.Clear();
            foreach (var region in found) Regions.Add(region);
        }
    }
}