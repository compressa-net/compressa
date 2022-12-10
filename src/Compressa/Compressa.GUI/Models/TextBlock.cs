using Compressa.Models.Metadata;

namespace Compressa.GUI.Models;

[INotifyPropertyChanged]
public partial class TextBlock
{
    [ObservableProperty]
    string id;

    [ObservableProperty]
    string title;

    [ObservableProperty]
    string subtitle;

    [ObservableProperty]
    string text;
}