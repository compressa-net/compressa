namespace Compressa.GUI.Pages;

[INotifyPropertyChanged]
public partial class YouTubeViewModel
{
    [ObservableProperty]
    ObservableCollection<Item> _products;

    [ObservableProperty]
    string category = ItemCategory.Business.ToString();

    partial void OnCategoryChanged(string cat)
    {
    }

    public YouTubeViewModel()
    {
    }
}