using System.Text.RegularExpressions;

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

        var chapters = AppData.Audiobooks.FirstOrDefault(ab => ab.Meta.Id == "aisuperpowers")?.Meta.Chapters;

        if (chapters == null)
        {
            return;
        }

        if (summaryType == "Chapters")
        {
            newTextBlocks = chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = ch.ParagraphSummary });
        }

        if (summaryType == "Bullet points")
        {
            newTextBlocks = chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = ch.BulletsVerboseSummary });
        }

        if (summaryType == "Summaries")
        {
            newTextBlocks = chapters.Select(ch => new TextBlock { Title = ch.Title, Subtitle = ch.GistSummary, Text = String.Join(Environment.NewLine + Environment.NewLine, ch.Segments.Select(s => s.ChatGPTResponse ?? "")), Sentiment = (ch.Segments.Length == 0 ? 0.0f : ch.Segments.Average(s => s.Sentiment?.Positive - s.Sentiment?.Negative ?? 0.0f)) });
        }

        if (newTextBlocks != null)
        {
            _textBlocks.Clear();
            foreach (var tb in newTextBlocks)
            {
                tb.Text = tb.Text?.Replace("- ", Environment.NewLine + "• ").Trim();
                if (tb.Text != null)
                {
                    tb.Text = Regex.Unescape(tb.Text);
                }
                _textBlocks.Add(tb);
            }
        }
    }

    public ReaderViewModel()
    {
        OnSummaryTypeChanged(summaryType);
    }
}