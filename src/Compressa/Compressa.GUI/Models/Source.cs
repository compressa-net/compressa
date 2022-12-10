namespace Compressa.GUI.Models;

[INotifyPropertyChanged]
public partial class Source
{
    [ObservableProperty]
    string name;

    [ObservableProperty]
    string description;

    [ObservableProperty]
    string image;

    [ObservableProperty]
    string itemCount;
}