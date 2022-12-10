using Compressa.GUI.Services;

namespace Compressa.GUI.Pages;

[INotifyPropertyChanged]
public partial class DashboardViewModel
{
    [RelayCommand]
    async Task ViewAll(ICompressaClientService compressaClientService)
    {
        await App.Current.MainPage.DisplayAlert("Not Implemented", "Wouldn't it be nice tho?", "Okay");
    }
}

