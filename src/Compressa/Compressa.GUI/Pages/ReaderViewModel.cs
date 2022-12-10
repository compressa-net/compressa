namespace Compressa.GUI.Pages;

[INotifyPropertyChanged]
public partial class ReaderViewModel
{
    [ObservableProperty]
    ObservableCollection<TextBlock> _textBlocks = new ObservableCollection<TextBlock>();

    [ObservableProperty]
    string summaryType = "Chapters";

    partial void OnSummaryTypeChanged(string summaryType)
    {
        IEnumerable<TextBlock> newTextBlocks = null;

        if (summaryType == "Chapters")
        {
            newTextBlocks = AppData.Audiobooks[0].Meta.Chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = ch.ParagraphSummary });
        }

        if (summaryType == "Bullet points")
        {
            newTextBlocks = AppData.Audiobooks[0].Meta.Chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = ch.BulletsVerboseSummary });
        }

        if (summaryType == "Summaries")
        {
            newTextBlocks = AppData.Audiobooks[0].Meta.Chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = String.Join(Environment.NewLine + Environment.NewLine, ch.Segments.Select(s => s.ChatGPTResponse ?? "")) });
        }

        if (newTextBlocks != null)
        {
            _textBlocks.Clear();
            foreach (var tb in newTextBlocks)
            {
                tb.Text = tb.Text?.Replace("-", Environment.NewLine + "•").Trim();                
                _textBlocks.Add(tb);
            }
        }
    }

    public ReaderViewModel()
    {
        OnSummaryTypeChanged(summaryType);
    }
}