namespace Compressa.GUI.Models;

[INotifyPropertyChanged]
public partial class Item
{
    [ObservableProperty]
    string title;

    [ObservableProperty]
    int quantity;

    [ObservableProperty]
    string image;

    [ObservableProperty]
    double price;

    [ObservableProperty]
    string subtitle;

    [ObservableProperty]
    string author;

    [ObservableProperty]
    string summary;

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