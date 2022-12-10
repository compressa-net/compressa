namespace Compressa.GUI.Pages;

[INotifyPropertyChanged]
public partial class HomeViewModel
{
    [ObservableProperty]
    ObservableCollection<Item> _products;

    [ObservableProperty]
    ObservableCollection<Source> _sources;

    [ObservableProperty]
    string category = ItemCategory.Business.ToString();

    partial void OnCategoryChanged(string cat)
    {
    }

    public HomeViewModel()
    {
        _products = new ObservableCollection<Item>(
            AppData.Audiobooks.ToList()
        );

        _sources = new ObservableCollection<Source>(
            AppData.Sources.ToList()
        );
    }
}