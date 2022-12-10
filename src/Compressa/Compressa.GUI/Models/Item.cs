using Compressa.Models.Metadata;

namespace Compressa.GUI.Models;

[INotifyPropertyChanged]
public partial class Item
{
    [ObservableProperty]
    string id;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    int quantity;

    [ObservableProperty]
    string imageFilename;

    [ObservableProperty]
    double price;

    [ObservableProperty]
    string subtitle;

    [ObservableProperty]
    string author;

    [ObservableProperty]
    string summary;

    [ObservableProperty]
    Audiobook meta;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(SubTotal));
    }

    public ItemCategory Category { get; set; }

    public double SubTotal
    {
        get
        {
            return Price * Quantity;
        }
    }
}