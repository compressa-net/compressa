﻿using Microcharts;
using SkiaSharp.Views.Maui;

namespace Compressa.GUI.Pages;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DrawChart();
    }

    private void DrawChart()
    {
        TypesChart.Chart = new RadialGaugeChart
        {
            LabelTextSize = 24,
            BackgroundColor = Colors.Transparent.ToSKColor(),
            IsAnimated = true,
            Entries = new List<ChartEntry>
            {
                new ChartEntry(10)
                {
                    Color = Color.FromArgb("#C9FBFA").ToSKColor()
                },
                new ChartEntry(51)
                {
                    Color = Color.FromArgb("#50D1AA").ToSKColor()
                },
                new ChartEntry(14)
                {
                    Color = Color.FromArgb("#FF7CA3").ToSKColor()
                },new ChartEntry(2)
                {
                    Color = Colors.WhiteSmoke.ToSKColor()
                }
            }
        };


    }

    async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {
        await DisplayAlert("Filter", "You are looking for a filter dialog that isn't yet implemented. Come back later", "Okay");
    }
}
