﻿namespace Compressa.GUI.Pages;

[INotifyPropertyChanged]
public partial class AudiobooksViewModel
{
    [ObservableProperty]
    ObservableCollection<Item> _products;

    [ObservableProperty]
    string category = ItemCategory.Business.ToString();

    partial void OnCategoryChanged(string cat)
    {
        ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), cat);
        _products = new ObservableCollection<Item>(
            AppData.Items.Where(x => x.Category == category).ToList()
        );
        OnPropertyChanged(nameof(Products));
    }

    public AudiobooksViewModel()
    {
        _products = new ObservableCollection<Item>(
            AppData.Items.Where(x => x.Category == ItemCategory.Business).ToList()
        );
    }

    [RelayCommand]
    async Task Preferences()
    {
        await Shell.Current.GoToAsync($"{nameof(EmptyPage)}?sub=appearance");
    }

    [RelayCommand]
    async Task AddProduct()
    {
        MessagingCenter.Send<AudiobooksViewModel, string>(this, "action", "add");
    }
}