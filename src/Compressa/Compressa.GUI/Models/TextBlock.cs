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

    [ObservableProperty]
    float sentiment;

    public bool IsSentimentPositive
    {
        get
        {
            return sentiment > 0.7;
        }
    }

    public bool IsSentimentNegative
    {
        get
        {
            return sentiment < 0.0;
        }
    }
}